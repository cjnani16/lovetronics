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
        chassis.Add(new Hardware(1, "Bulky", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0)));
        chassis.Add(new Hardware(2, "Focused", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 10)));
        chassis.Add(new Hardware(3, "Humanoid", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 5)));


        // Locomotion
        locomotion = new List<Hardware>();
        locomotion.Add(new Hardware(1, "Spider", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10)));
        locomotion.Add(new Hardware(2, "Wheel", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0)));
        locomotion.Add(new Hardware(3, "Chicken", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10)));
        locomotion.Add(new Hardware(4, "Legs", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20)));

        // Equipment
        equipment = new List<Hardware>();
        equipment.Add(new Hardware(1, "Slash", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 20)));
        equipment.Add(new Hardware(2, "Pew", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0)));
        equipment.Add(new Hardware(3, "Pow", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10)));


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
