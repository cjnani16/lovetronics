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
        // Chassis
        chassis = new List<Hardware>();
        chassis.Add(new Hardware("Humanoid", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 5)));
        chassis.Add(new Hardware("Focused", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 10)));
        chassis.Add(new Hardware("Bulky", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0)));


        // Locomotion
        locomotion = new List<Hardware>();
        locomotion.Add(new Hardware("Spider", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10)));
        locomotion.Add(new Hardware("Wheel", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0)));
        locomotion.Add(new Hardware("Legs", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20)));
        locomotion.Add(new Hardware("Chicken", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10)));

        // Equipment
        equipment = new List<Hardware>();
        equipment.Add(new Hardware("Pew", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0)));
        equipment.Add(new Hardware("Slash", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 20)));
        equipment.Add(new Hardware("Pow", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10)));


        // All
        allHardware = new List<Hardware>();
        allHardware.AddRange(chassis);
        allHardware.AddRange(locomotion);
        allHardware.AddRange(equipment);

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
