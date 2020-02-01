using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager = null;
    private PlayerStats baseStats;
    private List<Hardware> hardware;
    private Hardware selectedHardware;

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
        hardware = new List<Hardware> { chassis1, chassis2, loco1 };
        selectedHardware = chassis1;
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

    public Hardware getSelectedHardware()
    {
        return selectedHardware;
    }
}
