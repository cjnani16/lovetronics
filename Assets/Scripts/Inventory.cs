using System.Collections;
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
        //This is how you define abilities (name, description, power(in range 0-1), cost, sound type)
        Ability a1 = new Ability("Default Ability", "Some random basic description", 1, 15, "gun");
        //This is how you add effects to that ability (name, stat changes, duration)
        BuffDebuff e = new BuffDebuff("Attack Up",new PlayerStats(10,0,0,0,0),3);
        a1.AddEffect(e);

        // Chassis
        chassis = new List<Hardware>();
        Hardware c1 = new Hardware(1,3, "Bulky","Tough and durable.", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0));
        Hardware c2 = new Hardware(2,3, "Focused","Excellent cooling.", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 5));
        Hardware c3 = new Hardware(3,3, "Humanoid","Well-balanced.", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 2));

        chassis.Add(c1);
        chassis.Add(c2);
        chassis.Add(c3);


        // Locomotion
        locomotion = new List<Hardware>();
        Hardware l1 = new Hardware(1,3, "Spider", "Highly creepy.", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10));
        Hardware l2 = new Hardware(2,3, "Wheel", "Great for flat terrain.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0));
        Hardware l3 = new Hardware(3,3, "Chicken", "Bawk!", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10));
        Hardware l4 = new Hardware(4,3, "Legs", "Standard android legs.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20));

        locomotion.Add(l1);
        locomotion.Add(l2);
        locomotion.Add(l3);
        locomotion.Add(l4);

        // Equipment
        equipment = new List<Hardware>();
        Hardware e1 = new Hardware(1,3, "Slash", "A tough saber.", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 0));
        Hardware e2 = new Hardware(2,3, "Pew", "High-powered rifle.", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0));
        Hardware e3 = new Hardware(3,3, "Pow", "Blunt crushing instrument.", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10));

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
                a.AddEffect("3-turn Atk up, but less regen", new PlayerStats(10,0,0,0,-5),3);
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
