using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SelectOpponent : MonoBehaviour
{
    List<BattlerState> enemies;
    List<string> enemyNames;
    public GameObject EnemyPanelPrefab, CardsParent;
    public Sprite defaultSprite;
    int index=0;
    float initialX;
    GameObject LButton, RButton;

    BattlerState AssembleAnEnemy(float multiplier) {
        int i = Random.Range(0,3);
        Hardware enemy_chassis = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.CHASSIS)[i];
        i = Random.Range(0,3);
        Hardware enemy_locomotion = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.LOCOMOTION)[i];
        i = Random.Range(0,3);
        Hardware enemy_equipment = GameManager.manager.GetInventory().getAllHardwareOfCategory(Hardware.Category.EQUIPMENT)[i];
        

        List<Hardware> hwList = new List<Hardware>();
        hwList.Add(enemy_chassis);
        hwList.Add(enemy_equipment);
        hwList.Add(enemy_locomotion);

        PlayerStats stats = new PlayerStats(50, 50, 200, 100, 10);
        stats += enemy_chassis.getStatBoosts();
        stats += enemy_equipment.getStatBoosts();
        stats += enemy_locomotion.getStatBoosts();

        return new BattlerState(stats, hwList);
    }

    void Start()
    {

        enemies = new List<BattlerState>();
        enemyNames = new List<string>();

        //populate list of enemies
        enemies.Add(AssembleAnEnemy(1));
        enemyNames.Add("Random Bot 1");

        enemies.Add(AssembleAnEnemy(1));
        enemyNames.Add("Eviltron 233");

        enemies.Add(AssembleAnEnemy(1));
        enemyNames.Add("Bot suXXX");

        SetupButtons();
        CreateEnemyPanels();
    }

    void setOpponent() {
        GameManager.manager.setNextOpponent(enemies[index]);
    }

    void SetupButtons() {
        LButton = transform.Find("LButton").gameObject;
        RButton = transform.Find("RButton").gameObject;
        
        RButton.GetComponent<Button>().onClick.AddListener(()=>{index++; setOpponent(); });
        LButton.GetComponent<Button>().onClick.AddListener(()=>{index--; setOpponent(); });
    }

    void UpdateButtons() {
        LButton.SetActive(index>0);
        RButton.SetActive(index<enemies.Count-1);
    }

    void CreateEnemyPanels() {
        initialX = CardsParent.transform.position.x;
        for(int i=0; i<enemies.Count; i++) {
            GameObject thisPanel = GameObject.Instantiate(EnemyPanelPrefab, CardsParent.transform.position + (new Vector3(0 + 1200*i, 90)), Quaternion.identity);
            thisPanel.transform.SetParent(CardsParent.transform);

            thisPanel.transform.Find("BotName").GetComponent<Text>().text = enemyNames[i];
            Sprite loadedSprite = GameManager.manager.getSprite(enemies[i]);
            thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().sprite = (loadedSprite!=null)?loadedSprite:defaultSprite;

            float prevHeight = thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().GetComponent<RectTransform>().sizeDelta.y;
            thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().GetComponent<RectTransform>().sizeDelta = thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().GetComponent<Image>().sprite.rect.size;
            float moveDownAmt = prevHeight-thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().GetComponent<RectTransform>().sizeDelta.y;

            thisPanel.transform.Find("EnemyBotImage").GetComponent<Image>().transform.parent.position -= new Vector3(0,moveDownAmt/2);


            string statStr = "HP: " + enemies[i].getAlteredStats().getMaxHealth()
            + "\nATK: " + (enemies[i].getAlteredStats().getAttack())
            + "\nDEF: " + (enemies[i].getAlteredStats().getDefense())
            + "\nCOOL: " + (enemies[i].getAlteredStats().getMaxCoolant())
            + "\nRGN: " + (enemies[i].getAlteredStats().getCoolantRegen());

            thisPanel.transform.Find("Stats").GetComponent<Text>().text = statStr;
            
            thisPanel.transform.Find("HardwareIcons").Find("EquipmentIcon").GetComponent<Image>().sprite = enemies[i].hardware[0].sprite;
            thisPanel.transform.Find("HardwareIcons").Find("ChassisIcon").GetComponent<Image>().sprite = enemies[i].hardware[1].sprite;
            thisPanel.transform.Find("HardwareIcons").Find("LocomotionIcon").GetComponent<Image>().sprite = enemies[i].hardware[2].sprite;
        }
    }

    void UpdateEnemyPanels() {
        float targetX = -1200*index + initialX;
        if (CardsParent.transform.position.x - targetX > 250) {
            CardsParent.transform.position -= new Vector3(100,0);
        } else if ( CardsParent.transform.position.x - targetX < -250) {
            CardsParent.transform.position += new Vector3(100,0);
        }
        else {
            CardsParent.transform.position = new Vector3(targetX, CardsParent.transform.position.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemyPanels();
        UpdateButtons();
    }
}
