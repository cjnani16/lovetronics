using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPTextManager : MonoBehaviour
{
    public Text hpText;
    void Start()
    {
        PlayerStats stats = GameManager.manager.getBaseStats();
        hpText.text = "HP: " + stats.getMaxHealth();
    }
}
