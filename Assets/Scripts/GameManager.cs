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
        Hardware chassis1 = new Hardware("Basic", Hardware.Category.CHASSIS, new PlayerStats(5, 5, -15, -10, 10));
        Hardware chassis2 = new Hardware("Aggressive", Hardware.Category.CHASSIS, new PlayerStats(10, 0, -10, 10, 0));
        Hardware chassis3 = new Hardware("Useless", Hardware.Category.CHASSIS, new PlayerStats(0, 0, 0, 0, 0));
        Hardware chassis4 = new Hardware("Useless2", Hardware.Category.CHASSIS, new PlayerStats(1, 0, 0, 0, 0));
        Hardware chassis5 = new Hardware("Useless3", Hardware.Category.CHASSIS, new PlayerStats(0, 1, 0, 0, 0));

        Hardware loco1 = new Hardware("Wheel", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 10, 10, 5));
        Hardware equip1 = new Hardware("Sword", Hardware.Category.EQUIPMENT, new PlayerStats(30, -10, 0, 0, 0));
        hardware = new List<Hardware> { chassis1, chassis2, chassis3, chassis4, chassis5, loco1, equip1 };
        selectedChassis = chassis2;
        selectedLocomotion = loco1;
        selectedEquipment = equip1;
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
