using System.Collections;
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

    private Inventory inventory;

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

        selectedChassis = inventory.getAllHardwareOfCategory(Hardware.Category.CHASSIS)[0];
        selectedLocomotion = inventory.getAllHardwareOfCategory(Hardware.Category.LOCOMOTION)[0];
        selectedEquipment = inventory.getAllHardwareOfCategory(Hardware.Category.EQUIPMENT)[0];

        // Starting Inventory
        hardware = new List<Hardware> { selectedChassis, selectedLocomotion, selectedEquipment };

        baseStats = calculateBaseStats();

    }

    public PlayerStats calculateBaseStats()
    {
        PlayerStats defaultStats = new PlayerStats(50, 50, 200, 100, 10);
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
            Mathf.Max(hp, 0),
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
}
