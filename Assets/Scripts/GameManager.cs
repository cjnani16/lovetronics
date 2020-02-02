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
        baseStats = new PlayerStats(10, 30, 120, 50, 5);
        Hardware chassis1 = new Hardware("Basic", Hardware.Category.CHASSIS, new PlayerStats(5, 5, -15, -10, 10));
        Hardware chassis2 = new Hardware("Aggressive", Hardware.Category.CHASSIS, new PlayerStats(10, 0, -10, 10, 0));
        Hardware loco1 = new Hardware("Wheel", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 10, 10, 5));
        Hardware equip1 = new Hardware("Sword", Hardware.Category.EQUIPMENT, new PlayerStats(30, -10, 0, 0, 0));
        hardware = new List<Hardware> { chassis1, chassis2, loco1, equip1 };
        selectedChassis = chassis2;
        selectedLocomotion = loco1;
        selectedEquipment = equip1;
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
    }
    public void setSelectedLocomotion(Hardware locomotion)
    {
        selectedLocomotion = locomotion;
    }
    public void setSelectedEquipment(Hardware equipment)
    {
        selectedEquipment = equipment;
    }
}
