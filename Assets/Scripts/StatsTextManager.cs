using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsTextManager : MonoBehaviour
{
    public Text hpText;
    public Text atkText;
    public Text defText;
    public Text coolText;
    public Text rgnText;
    private int lastHP = -1;
    private int lastATK = -1;
    private int lastDEF = -1;
    private int lastCOOL = -1;
    private int lastRGN = -1;
    void Start()
    {
        PlayerStats stats = GameManager.manager.getBaseStats();
        updateHPText(stats.getMaxHealth());
        updateATKText(stats.getAttack());
        updateDEFText(stats.getDefense());
        updateCOOLText(stats.getMaxCoolant());
        updateRGNText(stats.getCoolantRegen());
    }

    // void Update()
    // {
    //     PlayerStats stats = GameManager.manager.getBaseStats();
    //     if (stats.getMaxHealth() != lastHP)
    //     {
    //         updateHPText(stats.getMaxHealth());
    //     }
    //     if (stats.getAttack() != lastATK)
    //     {
    //         updateATKText(stats.getAttack());
    //     }
    //     if (stats.getDefense() != lastDEF)
    //     {
    //         updateDEFText(stats.getDefense());
    //     }
    //     if (stats.getMaxCoolant() != lastCOOL)
    //     {
    //         updateCOOLText(stats.getMaxCoolant());
    //     }
    //     if (stats.getCoolantRegen() != lastRGN)
    //     {
    //         updateRGNText(stats.getCoolantRegen());
    //     }
    // }

    private void updateHPText(int newValue)
    {
        lastHP = newValue;
        hpText.text = "HP: " + newValue;
    }

    private void updateATKText(int newValue)
    {
        lastATK = newValue;
        atkText.text = "ATK: " + newValue;
    }
    private void updateDEFText(int newValue)
    {
        lastDEF = newValue;
        defText.text = "DEF: " + newValue;
    }
    private void updateCOOLText(int newValue)
    {
        lastCOOL = newValue;
        coolText.text = "COOL: " + newValue;
    }
    private void updateRGNText(int newValue)
    {
        lastRGN = newValue;
        rgnText.text = "RGN: " + newValue;
    }
}
