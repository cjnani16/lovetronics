using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class BattlerState {
    public float health, coolant;
    public PlayerStats stats;
    public BattlerState(PlayerStats s) {
        stats = s;
        health = stats.getMaxHealth();
        coolant = stats.getMaxCoolant();
    }

    public void ApplyTurnEndEffects() {
        coolant += stats.getCoolantRegen();
        if (coolant>stats.getMaxCoolant()) coolant=stats.getMaxCoolant();
    }
}

class MoveListener {
    BattleMove move; 
    public MoveListener(BattleMove m) {
        move = m;
    }

    public void Activate(ref BattlerState user, ref BattlerState target) {
        Debug.Log("using move: "+move.name +"to deal "+move.damageCalc(ref user, ref target));
        //do things with target and user
        user.coolant -= move.cost;
        target.health -= move.damageCalc(ref user, ref target);
        if (target.health<0) target.health=0;
        if (user.coolant<0) user.coolant=0;
    }
}

struct BattleMove {
    public string name, description;
    public float power, cost;
    public Color color;
    public BattleMove(string name, string description, float power, float cost) {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.color = Random.ColorHSV();
    }

    public float damageCalc(ref BattlerState user, ref BattlerState target) {
        return Mathf.Round(this.power*user.stats.getAttack()*(100f/(target.stats.getDefense()+100f)));
    }
}


public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject MoveListItemPrefab, UICanvas, HealthCoolantUI, PassTurnPrefab, PlayerBotImage, EnemyBotImage;

    GameObject currentHCUI;
    BattleState currentBattleState;
    BattlerState PlayerState, EnemyState;
    enum BattleState { Start, ChooseMove, AttackAnimation, EnemyAttackAnimation, Win, Loss };

    //runtinme populated
    List<GameObject> moveListPanels;
    GameObject enemyChosenMove;
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
            generatedMoves.Add(new BattleMove("Random Move"+i, "random description "+i, Random.Range(0.0f,1.0f), Random.Range(4,50)));
        }

        return generatedMoves;
    }

    List<BattleMove> EnemyDrawMoves() {
        List<BattleMove> generatedMoves = new List<BattleMove>();

        //add a couple random placeholder moves for now
        for (int i = 0; i <3; i++) {
            generatedMoves.Add(new BattleMove("Random Enemy Move"+i, "really mess you tf up!!! "+i, Random.Range(0.0f,1.0f), Random.Range(4,50)));
        }

        return generatedMoves;
    }

    void EnemyTurnAnnounce() {
        List<BattleMove> availableMoves = EnemyDrawMoves();
        BattleMove chosenMove = availableMoves[Random.Range(0,availableMoves.Count)];

        enemyChosenMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(1000, -1070, 0), Quaternion.identity);
        enemyChosenMove.transform.SetParent(UICanvas.transform,false);
        enemyChosenMove.GetComponent<Image>().color = new Color(1,0.2f,0.2f,1);

        //Set fields
        enemyChosenMove.transform.Find("NameText").GetComponent<Text>().text = chosenMove.name;
        enemyChosenMove.transform.Find("DescriptionText").GetComponent<Text>().text = chosenMove.description;
        enemyChosenMove.transform.Find("PowerText").GetComponent<Text>().text = ""+chosenMove.damageCalc(ref EnemyState, ref PlayerState);
        enemyChosenMove.transform.Find("CoolantText").GetComponent<Text>().text = "("+chosenMove.cost+")";

        MoveListener m = new MoveListener(chosenMove);
        m.Activate(ref EnemyState, ref PlayerState);
        
        PlayerBotImage.GetComponent<Animator>().Play("PlayerHurtAnimation");
        EnemyBotImage.GetComponent<Animator>().Play("EnemyAttackAnimation");
    }

    void EnemyCardUpdate() {
        if (enemyChosenMove.transform.localPosition.x>0)
            enemyChosenMove.transform.localPosition -= new Vector3(2000*Time.deltaTime,0);
        else {
            enemyChosenMove.transform.localPosition = new Vector3(0, enemyChosenMove.transform.localPosition.y);
        }
    }

    void EnemyCardDestroy() {
        GameObject.Destroy(enemyChosenMove);
    }

    void PresentMoves() {
        List<BattleMove> availableMoves = DrawMoves();

        panelYPositions = new float [availableMoves.Count+1];

        //add an option to skip ur turn
        GameObject skipMove = GameObject.Instantiate(PassTurnPrefab, new Vector3(0, -1370, 0), Quaternion.identity);
        skipMove.transform.SetParent(UICanvas.transform,false);
        skipMove.GetComponent<Image>().color = Color.yellow;
        //Set fields
        skipMove.transform.Find("CoolantText").GetComponent<Text>().text = "(+"+PlayerState.stats.getCoolantRegen()+")";
        skipMove.GetComponent<Button>().onClick.AddListener(() => { OnChoseAttack(true);});

        moveListPanels.Add(skipMove);
        panelYPositions[0] = 80;

        //put a button on screen for each move
        for (int i = 0; i<availableMoves.Count; i++) {
            GameObject thisMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(0, -1370, 0), Quaternion.identity);
            thisMove.transform.SetParent(UICanvas.transform,false);
            thisMove.GetComponent<Image>().color = Color.green;

            //Set fields
            thisMove.transform.Find("NameText").GetComponent<Text>().text = availableMoves[i].name;
            thisMove.transform.Find("DescriptionText").GetComponent<Text>().text = availableMoves[i].description;
            thisMove.transform.Find("PowerText").GetComponent<Text>().text = ""+availableMoves[i].damageCalc(ref PlayerState, ref EnemyState);
            thisMove.transform.Find("CoolantText").GetComponent<Text>().text = "("+availableMoves[i].cost+")";

            MoveListener m = new MoveListener(availableMoves[i]);

            //mana check
            if (PlayerState.coolant >= availableMoves[i].cost) {
                thisMove.GetComponent<Button>().onClick.AddListener(() => {m.Activate( ref this.PlayerState, ref this.EnemyState); OnChoseAttack(false);});
            } else {
                thisMove.GetComponent<Image>().color = Color.gray;
            }

            moveListPanels.Add(thisMove);
            panelYPositions[i+1] = 320+300*(i);
        }
    }

    void OnChoseAttack(bool passed) {
        if (!passed) {
            //animate
            PlayerBotImage.GetComponent<Animator>().Play("PlayerAttackAnimation");
            EnemyBotImage.GetComponent<Animator>().Play("EnemyHurtAnimation");
        }

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

    void CheckWinLossState() {
        if (PlayerState.health==0) {
            currentBattleState = BattleState.Loss;
        } else if (EnemyState.health==0) {
            currentBattleState = BattleState.Win;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentBattleState = BattleState.Start;

        //init health and coolant
        PlayerState = new BattlerState(GameManager.manager.getBaseStats());
        EnemyState = new BattlerState(new PlayerStats(66,66,66,66,66));
        Debug.Log("Player attack is :"+PlayerState.stats.getAttack());
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
                    CheckWinLossState();
                    if (currentBattleState!=BattleState.Win) EnemyTurnAnnounce();
                }
            } break;

            //pause for animation
            case BattleState.EnemyAttackAnimation: {
                t+=Time.deltaTime;
                EnemyCardUpdate();
                if (t>3) {
                    t=0;
                    currentBattleState = BattleState.ChooseMove;
                    
                    EnemyCardDestroy();
                    CheckWinLossState();
                    if (currentBattleState!=BattleState.Loss) PresentMoves();

                    //regen and other end of turn effects
                    PlayerState.ApplyTurnEndEffects();
                    EnemyState.ApplyTurnEndEffects();
                    riseSpeed=400;
                }
            } break;

            case BattleState.Loss: SceneManager.LoadScene("LoseBattleScene"); break;
            case BattleState.Win: SceneManager.LoadScene("WinBattleScene"); break;

            default: break;
        }
    }
}
