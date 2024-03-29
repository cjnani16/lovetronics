﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager = null;
    private PlayerStats baseStats;
    private List<Hardware> hardware;
    private Hardware selectedChassis;
    private Hardware selectedLocomotion;
    private Hardware selectedEquipment;
    private Hardware droppedPart;

    private Inventory inventory;
    private BattlerState nextOpponent;

    void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        inventory = new Inventory();

        Hardware c2 = inventory.getAllHardwareOfCategory(Hardware.Category.CHASSIS)[1];
        selectedChassis = c2; // inventory.getAllHardwareOfCategory(Hardware.Category.CHASSIS)[0];
        selectedLocomotion = inventory.getAllHardwareOfCategory(Hardware.Category.LOCOMOTION)[0];
        selectedEquipment = inventory.getAllHardwareOfCategory(Hardware.Category.EQUIPMENT)[0];

        Hardware c3 = inventory.getAllHardwareOfCategory(Hardware.Category.CHASSIS)[2];

        Hardware e3 = inventory.getAllHardwareOfCategory(Hardware.Category.EQUIPMENT)[2];
        Hardware l3 = inventory.getAllHardwareOfCategory(Hardware.Category.LOCOMOTION)[2];


        // Starting Inventory
        hardware = new List<Hardware> { c2, c3, selectedLocomotion, selectedEquipment, e3, l3 };

        baseStats = calculateBaseStats();

    }

    public PlayerStats calculateBaseStats()
    {
        PlayerStats defaultStats = new PlayerStats(100, 50, 150, 100, 10);
        List<PlayerStats> allStats = new List<PlayerStats> {
            defaultStats,
            selectedChassis.getStatBoosts(),
            selectedLocomotion.getStatBoosts(),
            selectedEquipment.getStatBoosts()
        };
        int atk = 0;
        int def = 0;
        int hp = 0;
        int cool = 0;
        int rgn = 0;
        for (int i = 0; i < allStats.Count; i++)
        {
            atk += allStats[i].getAttack();
            def += allStats[i].getDefense();
            hp += allStats[i].getMaxHealth();
            cool += allStats[i].getMaxCoolant();
            rgn += allStats[i].getCoolantRegen();
        }
        return new PlayerStats(
            Mathf.Max(atk, 0),
            Mathf.Max(def, 0),
            Mathf.Max(hp, 1),
            Mathf.Max(cool, 0),
            Mathf.Max(rgn, 0)
        );

    }

    public PlayerStats getBaseStats()
    {
        return baseStats;
    }

    public void setBaseStats(PlayerStats stats)
    {
        baseStats = stats;
    }

    public List<Hardware> getEquippedHardware()
    {
        List<Hardware> equipped = new List<Hardware>();
        equipped.Add(selectedEquipment);
        equipped.Add(selectedLocomotion);
        equipped.Add(selectedChassis);
        return equipped;
    }

    public List<Hardware> getHardware()
    {
        return hardware;
    }

    public Hardware getSelectedChassis()
    {
        return selectedChassis;
    }
    public Hardware getSelectedLocomotion()
    {
        return selectedLocomotion;
    }
    public Hardware getSelectedEquipment()
    {
        return selectedEquipment;
    }

    public Hardware getDroppedPart()
    {
        return droppedPart;
    }

    public void setDroppedPart(Hardware hw)
    {
        droppedPart = hw;
    }

    public void setSelectedChassis(Hardware chassis)
    {
        selectedChassis = chassis;
        baseStats = calculateBaseStats();
    }
    public void setSelectedLocomotion(Hardware locomotion)
    {
        selectedLocomotion = locomotion;
        baseStats = calculateBaseStats();
    }
    public void setSelectedEquipment(Hardware equipment)
    {
        selectedEquipment = equipment;
        baseStats = calculateBaseStats();
    }

    public void gainHardware(Hardware hw)
    {
        hardware.Add(hw);
    }

    public void setNextOpponent(BattlerState enemy)
    {
        this.nextOpponent = enemy;
    }

    public BattlerState getNextOpponent()
    {
        return this.nextOpponent;
    }

    public Sprite getSprite()
    {
        int chassisID = getSelectedChassis().id;
        int equipmentID = getSelectedEquipment().id;
        int locomotionID = getSelectedLocomotion().id;
        string sprite = "Bots/c" + chassisID + "_l" + locomotionID + "_e" + equipmentID;
        return Resources.Load<Sprite>(sprite);
    }

    public Sprite getSprite(BattlerState battler)
    {
        //hope they're in order lol
        int chassisID = battler.hardware[0].id;
        int equipmentID = battler.hardware[1].id;
        int locomotionID = battler.hardware[2].id;
        string sprite = "Bots/c" + chassisID + "_l" + locomotionID + "_e" + equipmentID;
        return Resources.Load<Sprite>(sprite);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
