﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Hardware> chassis;
    public List<Hardware> locomotion;
    public List<Hardware> equipment;
    public List<Hardware> allHardware;

    public Inventory()
    {
        BuildInventory();
    }

    private void BuildInventory()
    {
        Ability a1 = new Ability("Default Ability", "Some random basic thing", 1, 15, "gun");

        // Chassis
        chassis = new List<Hardware>();
        Hardware c1 = new Hardware(1, "Bulky","Tough and durable.", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0), "c2_test");
        Hardware c2 = new Hardware(2, "Focused","Excellent cooling.", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 5), "c2_test");
        Hardware c3 = new Hardware(3, "Humanoid","Well-balanced.", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 2), "c2_test");

        chassis.Add(c1);
        chassis.Add(c2);
        chassis.Add(c3);


        // Locomotion
        locomotion = new List<Hardware>();
        Hardware l1 = new Hardware(1, "Spider", "Highly creepy.", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10), "l3_test");
        Hardware l2 = new Hardware(2, "Wheel", "Great for flat terrain.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0), "l3_test");
        Hardware l3 = new Hardware(3, "Chicken", "Bawk!", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10), "l3_test");
        Hardware l4 = new Hardware(4, "Legs", "Standard android legs.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20), "l3_test");

        locomotion.Add(l1);
        locomotion.Add(l2);
        locomotion.Add(l3);
        locomotion.Add(l4);

        // Equipment
        equipment = new List<Hardware>();
        Hardware e1 = new Hardware(1, "Slash", "A tough saber.", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 0), "e2_test");
        Hardware e2 = new Hardware(2, "Pew", "High-powered rifle.", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0), "e2_test");
        Hardware e3 = new Hardware(3, "Pow", "Blunt crushing instrument.", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10), "e2_test");

        equipment.Add(e1);
        equipment.Add(e2);
        equipment.Add(e3);

        // All
        allHardware = new List<Hardware>();
        allHardware.AddRange(chassis);
        allHardware.AddRange(locomotion);
        allHardware.AddRange(equipment);

        //placeholder
        foreach (Hardware h in allHardware)
        {
            string sound;
            switch (h.name) {
                case "Slash":
                case "Chicken": sound="sword"; break;
                case "Pew": sound="gun"; break;
                default: sound="thud"; break;
            }

            Ability a = new Ability(h.name + " basic attaque", "smash em with ur " + h.name + "!", Random.Range(0.1f, 1), Random.Range(10, 100), sound);

            if (Random.Range(0,3)==2) {
                a.AddEffect("Basic 3-turn Atk boost at the cost of regen", new PlayerStats(10,0,0,0,-5),3);
            }

            h.addAbility(a);
        }

    }

    public List<Hardware> getAllHardware()
    {
        return allHardware;
    }

    public List<Hardware> getAllHardwareOfCategory(Hardware.Category category)
    {
        switch (category)
        {
            case Hardware.Category.CHASSIS:
                return chassis;
            case Hardware.Category.LOCOMOTION:
                return locomotion;
            case Hardware.Category.EQUIPMENT:
                return equipment;
        }
        return chassis;
    }
}
