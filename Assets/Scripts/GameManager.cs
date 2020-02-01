using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager = null;
    private PlayerStats baseStats;

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
    }

    public PlayerStats getBaseStats()
    {
        return baseStats;
    }

    public void setBaseStats(PlayerStats stats)
    {
        baseStats = stats;
    }
}
