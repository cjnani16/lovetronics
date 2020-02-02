using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattlerState
{
    public float health, coolant;

    //stats BEFORE buffs
    public PlayerStats stats;

    public List<BuffDebuff> buffsAndDebuffs;
    public List<Hardware> hardware;

    public BattlerState(PlayerStats s, List<Hardware> hw)
    {
        hardware = hw;
        buffsAndDebuffs = new List<BuffDebuff>();
        stats = s;
        health = stats.getMaxHealth();
        coolant = stats.getMaxCoolant();
    }

    //get stats AFTER buffs
    public PlayerStats getAlteredStats()
    {
        PlayerStats altered = stats;
        foreach (BuffDebuff b in buffsAndDebuffs)
        {
            altered += b.GetStatChanges();
        }
        return altered.zeroMin();
    }

    bool isExpired(BuffDebuff b)
    {
        return b.isExpired();
    }

    public void ApplyTurnEndEffects()
    {
        coolant += stats.getCoolantRegen();
        if (coolant > stats.getMaxCoolant()) coolant = stats.getMaxCoolant();

        //decrement timers on buffs/debuffs and remove expired ones
        foreach (BuffDebuff b in buffsAndDebuffs)
        {
            b.decrement();
        }
        buffsAndDebuffs.RemoveAll(isExpired);
    }

    public List<Ability> getAllAbilities()
    {
        List<Ability> list = new List<Ability>();
        foreach (Hardware hw in hardware)
        {
            list.AddRange(hw.abilities);
        }
        return list;
    }
}

class MoveListener
{
    Ability move;
    public MoveListener(Ability m)
    {
        move = m;
    }

    public void Activate(ref BattlerState user, ref BattlerState target)
    {
        Debug.Log("using move: " + move.name + "to deal " + move.damageCalc(ref user, ref target));
        //do things with target and user
        user.coolant -= move.cost;
        target.health -= move.damageCalc(ref user, ref target);
        if (target.health < 0) target.health = 0;
        if (user.coolant < 0) user.coolant = 0;
        move.playSound();

        //place buffs and debuffs onto the user
        foreach (BuffDebuff b in move.appliedEffects)
        {
            user.buffsAndDebuffs.Add(b);
        }
    }
}

/*struct BattleMove {
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

    
}*/


public class BattleManager : MonoBehaviour
{
    [SerializeField] public GameObject MoveListItemPrefab, UICanvas, HealthCoolantUI, PassTurnPrefab, PlayerBotImage, EnemyBotImage, BuffDebuffIndicatorPrefab;

    GameObject currentHCUI;
    BattleState currentBattleState;
    BattlerState PlayerState, EnemyState;
    enum BattleState { Start, ChooseMove, AttackAnimation, EnemyAttackAnimation, Win, Loss };

    //runtinme populated
    List<GameObject> moveListPanels;
    GameObject enemyChosenMove, playerBuffsAndDebuffsText, enemyBuffsAndDebuffsText;
    float[] panelYPositions;
    float riseSpeed = 400;

    void CreateBuffDebuffs()
    {
        playerBuffsAndDebuffsText = GameObject.Instantiate(BuffDebuffIndicatorPrefab, new Vector3(-169, -257), Quaternion.identity);
        playerBuffsAndDebuffsText.transform.SetParent(UICanvas.transform, false);
        playerBuffsAndDebuffsText.GetComponent<Text>().alignment = TextAnchor.UpperLeft;

        enemyBuffsAndDebuffsText = GameObject.Instantiate(BuffDebuffIndicatorPrefab, new Vector3(177, -257), Quaternion.identity);
        enemyBuffsAndDebuffsText.transform.SetParent(UICanvas.transform, false);
        enemyBuffsAndDebuffsText.GetComponent<Text>().alignment = TextAnchor.UpperRight;
    }
    void DrawBuffDebuffs()
    {
        //player b/ds
        PlayerStats totalChanges = new PlayerStats(0, 0, 0, 0, 0);
        for (int i = 0; i < PlayerState.buffsAndDebuffs.Count; i++)
        {
            totalChanges += PlayerState.buffsAndDebuffs[i].GetStatChanges();
        }
        playerBuffsAndDebuffsText.GetComponent<Text>().text = totalChanges.stringify();


        //enemy b/ds
        PlayerStats eTotalChanges = new PlayerStats(0, 0, 0, 0, 0);
        for (int i = 0; i < EnemyState.buffsAndDebuffs.Count; i++)
        {
            eTotalChanges += EnemyState.buffsAndDebuffs[i].GetStatChanges();
        }
        enemyBuffsAndDebuffsText.GetComponent<Text>().text = eTotalChanges.stringify();
    }

    void InitHealthCoolantUI()
    {
        currentHCUI = Instantiate(HealthCoolantUI, new Vector3(0, 150, 0), Quaternion.identity);
        currentHCUI.transform.SetParent(UICanvas.transform, false);
    }
    void UpdateHealthCoolantUI()
    {
        //player bars
        currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale = new Vector3(PlayerState.health / PlayerState.getAlteredStats().getMaxHealth(), 1, 1);
        if (currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale.x > currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale.x)
            currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale -= new Vector3(0.01f, 0, 0);
        else
            currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarBleed").GetComponent<RectTransform>().localScale = currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarFront").GetComponent<RectTransform>().localScale;

        currentHCUI.transform.Find("PlayerCBarBack").Find("PlayerCBarFront").GetComponent<RectTransform>().localScale = new Vector3(PlayerState.coolant / PlayerState.getAlteredStats().getMaxCoolant(), 1, 1);
        currentHCUI.transform.Find("PlayerCBarBack").Find("PlayerCBarLabel").GetComponent<Text>().text = "" + PlayerState.coolant;
        currentHCUI.transform.Find("PlayerHBarBack").Find("PlayerHBarLabel").GetComponent<Text>().text = "" + PlayerState.health;

        //enemy bars
        currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale = new Vector3(EnemyState.health / EnemyState.getAlteredStats().getMaxHealth(), 1, 1);
        if (currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale.x > currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale.x)
            currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale -= new Vector3(0.01f, 0, 0);
        else
            currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarBleed").GetComponent<RectTransform>().localScale = currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarFront").GetComponent<RectTransform>().localScale;

        currentHCUI.transform.Find("EnemyCBarBack").Find("EnemyCBarFront").GetComponent<RectTransform>().localScale = new Vector3(EnemyState.coolant / EnemyState.getAlteredStats().getMaxCoolant(), 1, 1);
        currentHCUI.transform.Find("EnemyCBarBack").Find("EnemyCBarLabel").GetComponent<Text>().text = "" + EnemyState.coolant;
        currentHCUI.transform.Find("EnemyHBarBack").Find("EnemyHBarLabel").GetComponent<Text>().text = "" + EnemyState.health;

    }

    List<Ability> DrawMoves()
    {
        List<Ability> movePool = PlayerState.getAllAbilities();
        List<Ability> drawnMoves = new List<Ability>();

        while (drawnMoves.Count < 3)
        {
            int index = Random.Range(0, movePool.Count);
            drawnMoves.Add(movePool[index]);
            movePool.RemoveAt(index);
        }

        return drawnMoves;
    }

    List<Ability> EnemyDrawMoves()
    {
        List<Ability> movePool = EnemyState.getAllAbilities();
        List<Ability> drawnMoves = new List<Ability>();

        while (drawnMoves.Count < 3)
        {
            int index = Random.Range(0, movePool.Count);
            drawnMoves.Add(movePool[index]);
            movePool.RemoveAt(index);
        }

        return drawnMoves;
    }

    void EnemyTurnAnnounce()
    {
        List<Ability> availableMoves = EnemyDrawMoves();
        Ability chosenMove = availableMoves[Random.Range(0, availableMoves.Count)];

        while (chosenMove.cost > EnemyState.coolant)
        {
            availableMoves.Remove(chosenMove);
            chosenMove = availableMoves.Count > 0 ? availableMoves[Random.Range(0, availableMoves.Count)] : new Ability("Pass", "Skip a turn to cool down", 0, 0, "");
        }

        if (chosenMove.name != "Pass")
        {
            enemyChosenMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(1000, -1070, 0), Quaternion.identity);
            enemyChosenMove.transform.SetParent(UICanvas.transform, false);
            enemyChosenMove.GetComponent<Image>().color = new Color(1, 0.2f, 0.2f, 1);

            //Set fields
            enemyChosenMove.transform.Find("NameText").GetComponent<Text>().text = chosenMove.name;
            enemyChosenMove.transform.Find("DescriptionText").GetComponent<Text>().text = chosenMove.description;
            enemyChosenMove.transform.Find("PowerText").GetComponent<Text>().text = "" + chosenMove.damageCalc(ref EnemyState, ref PlayerState);
            enemyChosenMove.transform.Find("CoolantText").GetComponent<Text>().text = "(" + chosenMove.cost + ")";

            MoveListener m = new MoveListener(chosenMove);
            m.Activate(ref EnemyState, ref PlayerState);

            PlayerBotImage.GetComponent<Animator>().Play("PlayerHurtAnimation");
            EnemyBotImage.GetComponent<Animator>().Play("EnemyAttackAnimation");
        }
        else
        {
            enemyChosenMove = GameObject.Instantiate(PassTurnPrefab, new Vector3(1000, -1070, 0), Quaternion.identity);
            enemyChosenMove.transform.SetParent(UICanvas.transform, false);
            enemyChosenMove.GetComponent<Image>().color = Color.yellow;

            //Set fields
            enemyChosenMove.transform.Find("CoolantText").GetComponent<Text>().text = "(+" + 2 * EnemyState.getAlteredStats().getCoolantRegen() + ")";

            //Give them the passing buff
            EnemyState.buffsAndDebuffs.Add(new BuffDebuff("Passed a Turn", new PlayerStats(0, 0, 0, 0, (EnemyState.getAlteredStats().getCoolantRegen())), 1));
        }

    }

    void EnemyCardUpdate()
    {
        if (enemyChosenMove.transform.localPosition.x > 0)
            enemyChosenMove.transform.localPosition -= new Vector3(2000 * Time.deltaTime, 0);
        else
        {
            enemyChosenMove.transform.localPosition = new Vector3(0, enemyChosenMove.transform.localPosition.y);
        }
    }

    void EnemyCardDestroy()
    {
        GameObject.Destroy(enemyChosenMove);
    }

    void PresentMoves()
    {
        List<Ability> availableMoves = DrawMoves();

        panelYPositions = new float[availableMoves.Count + 1];

        //add an option to skip ur turn
        GameObject skipMove = GameObject.Instantiate(PassTurnPrefab, new Vector3(0, -1370, 0), Quaternion.identity);
        skipMove.transform.SetParent(UICanvas.transform, false);
        skipMove.GetComponent<Image>().color = Color.yellow;
        //Set fields
        skipMove.transform.Find("CoolantText").GetComponent<Text>().text = "(+" + 2 * PlayerState.getAlteredStats().getCoolantRegen() + ")";
        skipMove.GetComponent<Button>().onClick.AddListener(() => { OnChoseAttack(true); });

        moveListPanels.Add(skipMove);
        panelYPositions[0] = 80;

        //put a button on screen for each move
        for (int i = 0; i < availableMoves.Count; i++)
        {
            GameObject thisMove = GameObject.Instantiate(MoveListItemPrefab, new Vector3(0, -1370, 0), Quaternion.identity);
            thisMove.transform.SetParent(UICanvas.transform, false);
            thisMove.GetComponent<Image>().color = Color.green;

            //Set fields
            thisMove.transform.Find("NameText").GetComponent<Text>().text = availableMoves[i].name;
            thisMove.transform.Find("DescriptionText").GetComponent<Text>().text = availableMoves[i].description + "\n";
            thisMove.transform.Find("PowerText").GetComponent<Text>().text = "" + availableMoves[i].damageCalc(ref PlayerState, ref EnemyState);
            thisMove.transform.Find("CoolantText").GetComponent<Text>().text = "(" + availableMoves[i].cost + ")";

            //Add efffects to description
            foreach (BuffDebuff b in availableMoves[i].appliedEffects)
            {
                thisMove.transform.Find("DescriptionText").GetComponent<Text>().text += b.name;
            }

            MoveListener m = new MoveListener(availableMoves[i]);

            //mana check
            if (PlayerState.coolant >= availableMoves[i].cost)
            {
                thisMove.GetComponent<Button>().onClick.AddListener(() => { m.Activate(ref this.PlayerState, ref this.EnemyState); OnChoseAttack(false); });
            }
            else
            {
                thisMove.GetComponent<Image>().color = Color.gray;
            }

            moveListPanels.Add(thisMove);
            panelYPositions[i + 1] = 320 + 300 * (i);
        }
    }

    void OnChoseAttack(bool passed)
    {
        if (!passed)
        {
            //animate
            PlayerBotImage.GetComponent<Animator>().Play("PlayerAttackAnimation");
            EnemyBotImage.GetComponent<Animator>().Play("EnemyHurtAnimation");
        }
        else
        {
            PlayerState.buffsAndDebuffs.Add(new BuffDebuff("Passed a Turn", new PlayerStats(0, 0, 0, 0, (PlayerState.getAlteredStats().getCoolantRegen())), 1));
        }

        this.currentBattleState = BattleState.AttackAnimation;
        foreach (GameObject g in moveListPanels)
        {
            GameObject.Destroy(g);
        }
        moveListPanels.Clear();
        Debug.Log("Start player attack animation (3s)");
    }

    void UpdateMoveList()
    {
        for (int i = 0; i < moveListPanels.Count; i++)
        {
            if (moveListPanels[i].transform.position.y < panelYPositions[i])
            {
                moveListPanels[i].transform.position += new Vector3(0, riseSpeed * Time.deltaTime);
            }
            else if (moveListPanels[i].transform.position.y != panelYPositions[i])
            {
                moveListPanels[i].transform.position = new Vector3(moveListPanels[i].transform.position.x, panelYPositions[i]);
            }
        }
        riseSpeed += 20;
    }

    void CheckWinLossState()
    {
        if (PlayerState.health == 0)
        {
            currentBattleState = BattleState.Loss;
        }
        else if (EnemyState.health == 0)
        {
            currentBattleState = BattleState.Win;
        }
    }

    void AssembleAnEnemy()
    {
        int i = Random.Range(0, 3);
        Hardware enemy_chassis = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.CHASSIS)[i];
        i = Random.Range(0, 3);
        Hardware enemy_locomotion = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.LOCOMOTION)[i];
        i = Random.Range(0, 3);
        Hardware enemy_equipment = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.EQUIPMENT)[i];


        List<Hardware> hwList = new List<Hardware>();
        hwList.Add(enemy_chassis);
        hwList.Add(enemy_equipment);
        hwList.Add(enemy_locomotion);

        PlayerStats stats = new PlayerStats(100, 50, 150, 100, 10);
        stats += enemy_chassis.getStatBoosts();
        stats += enemy_equipment.getStatBoosts();
        stats += enemy_locomotion.getStatBoosts();

        EnemyState = new BattlerState(stats, hwList);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentBattleState = BattleState.Start;

        //init health and coolant
        PlayerState = new BattlerState(GameManager.manager.getBaseStats(), GameManager.manager.getEquippedHardware());

        Debug.Log("Player hw:");
        foreach (Hardware hw in PlayerState.hardware)
        {
            Debug.Log(hw.name);
        }

        AssembleAnEnemy();

        //set appropriate sprite, keep all sprites on equal ground height
        Sprite defaultSprite = PlayerBotImage.GetComponent<Image>().sprite;
        Sprite loadedSprite = GameManager.manager.getSprite();
        PlayerBotImage.GetComponent<Image>().sprite = (loadedSprite == null) ? defaultSprite : loadedSprite;
        float prevHeight = PlayerBotImage.GetComponent<RectTransform>().sizeDelta.y;
        PlayerBotImage.GetComponent<RectTransform>().sizeDelta = PlayerBotImage.GetComponent<Image>().sprite.rect.size;
        float moveDownAmt = prevHeight - PlayerBotImage.GetComponent<RectTransform>().sizeDelta.y;

        PlayerBotImage.transform.parent.position -= new Vector3(0, moveDownAmt / 2);

        CreateBuffDebuffs();
        InitHealthCoolantUI();

        moveListPanels = new List<GameObject>();
    }

    float t = 0;
    // Update is called once per frame
    void Update()
    {
        UpdateHealthCoolantUI();
        DrawBuffDebuffs();

        switch (currentBattleState)
        {
            //battle start animation?
            case BattleState.Start: currentBattleState = BattleState.ChooseMove; PresentMoves(); break;

            case BattleState.ChooseMove: UpdateMoveList(); break;

            //pause for animation
            case BattleState.AttackAnimation:
                {
                    t += Time.deltaTime;
                    if (t > 2)
                    {
                        t = 0;
                        currentBattleState = BattleState.EnemyAttackAnimation;
                        Debug.Log("Start enemy attack animation (3s)");
                        CheckWinLossState();
                        if (currentBattleState != BattleState.Win) EnemyTurnAnnounce();
                        else EnemyBotImage.GetComponent<Animator>().Play("EnemyDeathAnimation");
                    }
                }
                break;

            //pause for animation
            case BattleState.EnemyAttackAnimation:
                {
                    t += Time.deltaTime;
                    EnemyCardUpdate();
                    if (t > 2)
                    {
                        t = 0;
                        currentBattleState = BattleState.ChooseMove;

                        EnemyCardDestroy();
                        CheckWinLossState();

                        //regen and other end of turn effects
                        PlayerState.ApplyTurnEndEffects();
                        EnemyState.ApplyTurnEndEffects();

                        if (currentBattleState != BattleState.Loss) PresentMoves();
                        else PlayerBotImage.GetComponent<Animator>().Play("PlayerDeathAnimation");

                        riseSpeed = 400;
                    }
                }
                break;

            case BattleState.Loss:
                {
                    t += Time.deltaTime;
                    if (t > 2) SceneManager.LoadScene("LoseBattleScene"); break;
                }
            case BattleState.Win:
                {
                    t += Time.deltaTime;
                    if (t > 2)
                    {
                        int i = Random.Range(0, EnemyState.hardware.Count);
                        GameManager.manager.setDroppedPart(EnemyState.hardware[i].rollSimilar());
                        GameManager.manager.gainHardware(GameManager.manager.getDroppedPart());
                        SceneManager.LoadScene("WinBattleScene");
                    }
                    break;
                }

            default: break;
        }
    }
}
