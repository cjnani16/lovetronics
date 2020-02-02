﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HardwareRowController : MonoBehaviour
{
    public Hardware hardware;
    public HardwarePanelController controller;

    void Start()
    {
        Text name = transform.Find("Name").gameObject.GetComponent<Text>();
        name.text = hardware.name;
        PlayerStats stats = hardware.statBoosts;
        Text statBoosts = transform.Find("StatBoosts").gameObject.GetComponent<Text>();
        statBoosts.text = "HP: " + getStringFromInt(stats.getMaxHealth())
        + "\nATK: " + getStringFromInt(stats.getAttack())
        + "\nDEF: " + getStringFromInt(stats.getDefense())
        + "\nCOOL: " + getStringFromInt(stats.getMaxCoolant())
        + "\nRGN: " + getStringFromInt(stats.getCoolantRegen());

        setBackgroundColor();
    }


    public void setSelectedHardware()
    {
        switch (hardware.category)
        {
            case Hardware.Category.CHASSIS:
                Debug.Log("setting selected chassis to " + hardware.name);
                GameManager.manager.setSelectedChassis(hardware);
                break;
            case Hardware.Category.LOCOMOTION:
                GameManager.manager.setSelectedLocomotion(hardware);
                break;
            case Hardware.Category.EQUIPMENT:
                GameManager.manager.setSelectedEquipment(hardware);
                break;
        }
        controller.updateSelectedStates();
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

    private bool isSelected()
    {
        switch (hardware.category)
        {
            case Hardware.Category.CHASSIS:
                return hardware.name == GameManager.manager.getSelectedChassis().name;
            case Hardware.Category.LOCOMOTION:
                return hardware.name == GameManager.manager.getSelectedLocomotion().name;
            case Hardware.Category.EQUIPMENT:
                return hardware.name == GameManager.manager.getSelectedEquipment().name;
        }
        return false;
    }

    public void setBackgroundColor()
    {
        Debug.Log(hardware.name);
        Debug.Log(GameManager.manager.getSelectedChassis().name);
        Image border = transform.Find("HardwareBorder").gameObject.GetComponent<Image>();
        Image background = transform.Find("HardwareBackground").gameObject.GetComponent<Image>();
        if (isSelected())
        {
            border.color = new Color32(20, 70, 15, 255);
            background.color = new Color32(20, 170, 20, 255);
        }
        else
        {
            border.color = Color.black;
            background.color = Color.white;
        }
    }
}
