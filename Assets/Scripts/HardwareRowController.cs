using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareRowController : MonoBehaviour
{
    public Hardware hardware;

    void Start()
    {
        GameObject info = transform.Find("HardwareInfo").gameObject;
        Text name = info.transform.Find("Name").gameObject.GetComponent<Text>();
        Debug.Log(hardware.name);
        name.text = hardware.name;
        PlayerStats stats = hardware.statBoosts;
        Text statBoosts = info.transform.Find("StatBoosts").gameObject.GetComponent<Text>();
        statBoosts.text = "HP: " + getStringFromInt(stats.getMaxHealth())
        + "\nATK: " + getStringFromInt(stats.getAttack())
        + "\nDEF: " + getStringFromInt(stats.getDefense())
        + "\nCOOL: " + getStringFromInt(stats.getMaxCoolant())
        + "\nRGN: " + getStringFromInt(stats.getCoolantRegen());
    }

    private string getStringFromInt(int value)
    {
        if (value > 0)
        {
            return "+" + value;
        }
        else
        {
            return "" + value;
        }
    }
}
