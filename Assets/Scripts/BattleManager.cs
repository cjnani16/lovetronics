using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class BattlerState {
    public float health, coolant;
    public PlayerStats stats;
    public BattlerState(PlayerStats s) {
        stats = s;
        health = stats.getMaxHealth();
        coolant = stats.getMaxCoolant();
    }
}

class MoveListener {
    BattleMove move; 
    public MoveListener(BattleMove m) {
        move = m;
    }

    public void Activate(ref BattlerState user, ref BattlerState target) {
        Debug.Log("using move: "+move.name);
        //do things with target and user
        user.coolant -= move.cost;
        target.health -= move.power;
    }
}

struct BattleMove {
    public string name, description;
    public int power, cost;
    public Color color;
    public BattleMove(string name, string description, int power, int cost) {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.color = Random.ColorHSV();
    }
}


public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject MoveListItemPrefab, UICanvas, HealthCoolantUI;

    GameObject currentHCUI;
    BattleState currentBattleState;
    BattlerState PlayerState, EnemyState;
    enum BattleState { Start, ChooseMove, AttackAnimation, EnemyAttackAnimation, End };

    //runtinme populated
    List<GameObject> moveListPanels;
    float [] panelYPositions;
    float riseSpeed=400;

    void InitHealthCoolantUI() {
        currentHCUI = Instantiate(HealthCoolantUI, Vector3.zero, Quaternion.identity);
        currentHCUI.transform.SetParent(UICanvas.transform,false);
    }
    void UpdateHealthCoolantUI() {
        //player bars
        currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale = new Vector3(PlayerState.health/PlayerState.stats.getMaxHealth(),1,1);
        if (currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale.x > currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale.x) 
           currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale -= new Vector3(0.01f,0,0);
        else
           currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale = currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale;
        
        currentHCUI.transform.Find("PlayerCBarBack").Find("PlayerCBarFront").GetComponent<RectTransform>().localScale = new Vector3(PlayerState.coolant/PlayerState.stats.getMaxCoolant(),1,1);
        currentHCUI.transform.Find("PlayerCBarBack").Find("PlayerCBarLabel").GetComponent<Text>().text = ""+PlayerState.coolant;
        currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarLabel").GetComponent<Text>().text = ""+PlayerState.health;

        //enemy bars
        currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale = new Vector3(EnemyState.health/EnemyState.stats.getMaxHealth(),1,1);
        if (currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale.x > currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale.x) 
           currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale -= new Vector3(0.01f,0,0);
        else
           currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale = currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale;
        
        currentHCUI.transform.Find("EnemyCBarBack").Find("EnemyCBarFront").GetComponent<RectTransform>().localScale = new Vector3(EnemyState.coolant/EnemyState.stats.getMaxCoolant(),1,1);
        currentHCUI.transform.Find("EnemyCBarBack").Find("EnemyCBarLabel").GetComponent<Text>().text = ""+EnemyState.coolant;
        currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarLabel").GetComponent<Text>().text = ""+EnemyState.health;

    }
    
    List<BattleMove> DrawMoves() {
        List<BattleMove> generatedMoves = new List<BattleMove>();

        //add a couple random placeholder moves for now
        for (int i = 0; i <3; i++) {
            generatedMoves.Add(new BattleMove("Random Move"+i, "random description "+i, Random.Range(10,50), Random.Range(4,50)));
        }

        return generatedMoves;
    }

    void PresentMoves() {
        List<BattleMove> availableMoves = DrawMoves();

        panelYPositions = new float [availableMoves.Count];

        //put a button on screen for each move
        for (int i = 0; i<availableMoves.Count; i++) {
            GameObject thisMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(0, -1370, 0), Quaternion.identity);
            thisMove.transform.SetParent(UICanvas.transform,false);
            Image img = thisMove.GetComponent<Image>();
            img.color = Color.green;

            //Set fields
            thisMove.transform.Find("NameText").GetComponent<Text>().text = availableMoves[i].name;
            thisMove.transform.Find("DescriptionText").GetComponent<Text>().text = availableMoves[i].description;
            thisMove.transform.Find("PowerText").GetComponent<Text>().text = ""+availableMoves[i].power;
            thisMove.transform.Find("CoolantText").GetComponent<Text>().text = "("+availableMoves[i].cost+")";

            MoveListener m = new MoveListener(availableMoves[i]);

            //mana check
            if (PlayerState.coolant >= availableMoves[i].cost) {
                thisMove.GetComponent<Button>().onClick.AddListener(() => {m.Activate( ref this.PlayerState, ref this.EnemyState); OnChoseAttack();});
            } else {
                img.color = Color.gray;
            }

            moveListPanels.Add(thisMove);
            panelYPositions[i] = 150+300*i;
        }

    }

    void OnChoseAttack() {
        this.currentBattleState = BattleState.AttackAnimation;
        foreach (GameObject g in moveListPanels) {
            GameObject.Destroy(g);
        }
        moveListPanels.Clear();
        Debug.Log("Start player attack animation (3s)");
    }

    void UpdateMoveList() {
        for (int i=0; i< moveListPanels.Count; i++) {
            if (moveListPanels[i].transform.position.y < panelYPositions[i]) {
                moveListPanels[i].transform.position += new Vector3(0,riseSpeed*Time.deltaTime);
            } else if (moveListPanels[i].transform.position.y != panelYPositions[i]) {
                moveListPanels[i].transform.position = new Vector3(moveListPanels[i].transform.position.x,panelYPositions[i]);
            }
        }
        riseSpeed+=20;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentBattleState = BattleState.Start;

        //init health and coolant
        PlayerState = new BattlerState(GameManager.manager.getBaseStats());
        EnemyState = new BattlerState(new PlayerStats(66,66,66,66,66));
        InitHealthCoolantUI();

        moveListPanels = new List<GameObject>();
    }

    float t = 0;
    // Update is called once per frame
    void Update()
    {
        UpdateHealthCoolantUI();

        switch (currentBattleState) {
            //battle start animation?
            case BattleState.Start: currentBattleState = BattleState.ChooseMove; PresentMoves(); break;

            case BattleState.ChooseMove: UpdateMoveList(); break;

            //pause for animation
            case BattleState.AttackAnimation: {
                t+=Time.deltaTime;
                if (t>3) {
                    t=0;
                    currentBattleState = BattleState.EnemyAttackAnimation;
                    Debug.Log("Start enemy attack animation (3s)");
                }
            } break;

            //pause for animation
            case BattleState.EnemyAttackAnimation: {
                t+=Time.deltaTime;
                if (t>3) {
                    t=0;
                    currentBattleState = BattleState.ChooseMove;
                    PresentMoves();
                    riseSpeed=400;
                }
            } break;

            default: break;
        }
    }
}
