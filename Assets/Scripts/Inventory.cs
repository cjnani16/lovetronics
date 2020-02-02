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
        Ability a1 = new Ability("Default Ability", "Some random basic thing",1,15);

        // Chassis
        chassis = new List<Hardware>();
        Hardware c1 = new Hardware(1, "Bulky", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0));
        Hardware c2 = new Hardware(2, "Focused", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 10));
        Hardware c3 = new Hardware(3, "Humanoid", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 5));

        chassis.Add(c1);
        chassis.Add(c2);
        chassis.Add(c3);


        // Locomotion
        locomotion = new List<Hardware>();
        Hardware l1 = new Hardware(1, "Spider", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10));
        Hardware l2 = new Hardware(2, "Wheel", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0));
        Hardware l3 = new Hardware(3, "Chicken", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10));
        Hardware l4 = new Hardware(4, "Legs", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20));

        locomotion.Add(l1);
        locomotion.Add(l2);
        locomotion.Add(l3);
        locomotion.Add(l4);

        // Equipment
        equipment = new List<Hardware>();
        Hardware e1 = new Hardware(1, "Slash", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 20));
        Hardware e2 = new Hardware(2, "Pew", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0));
        Hardware e3 = new Hardware(3, "Pow", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10));

        equipment.Add(e1);
        equipment.Add(e2);
        equipment.Add(e3);

        // All
        allHardware = new List<Hardware>();
        allHardware.AddRange(chassis);
        allHardware.AddRange(locomotion);
        allHardware.AddRange(equipment);

        //placeholder
        foreach (Hardware h in allHardware) {
            h.addAbility(new Ability(h.name+" basic attaque", "smash em with ur "+h.name, Random.Range(0.1f,1), Random.Range(10,100)));
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
