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

        // Bulk - Chassis
        // Heal
        Ability bulk1 = new Ability("Body Slam", "Splat", 0.6f, 60, "thud");
        // Take 30 damage
        Ability bulk2 = new Ability("Defense Protocol", "Shields online", 0.0f, 10, "thud");
        BuffDebuff DPE = new BuffDebuff("DEF Up", new PlayerStats(0, 10, 0, 0, 0), 4);
        bulk2.AddEffect(DPE);

        // Focused - Chassis
        Ability focused1 = new Ability("Mah Lazer", "I'mma firin' it", 0.8f, 75, "pew");
        Ability focused2 = new Ability("Scan", "Searching...", 0.0f, 10, "pew");
        BuffDebuff f2e = new BuffDebuff("ATK Up", new PlayerStats(10, 0, 0, 0, 0), 3);
        focused2.AddEffect(f2e);
        Ability focused3 = new Ability("System Optimization", "Running at 100%", 0.0f, 40, "thud");
        BuffDebuff f3e = new BuffDebuff("ATK, DEF, RGN Up", new PlayerStats(10, 10, 0, 0, 10), 2);
        focused3.AddEffect(f3e);

        // Humanoid - Chassis
        Ability humanoid1 = new Ability("Attack Protocol", "Safety protocol disengaged", 0.2f, 30, "sword");
        BuffDebuff h1e = new BuffDebuff("ATK Up", new PlayerStats(10, 0, 0, 0, 0), 3);
        humanoid1.AddEffect(h1e);
        // Ability humanoid2 = new Ability("Loose Wires", "It wasn't supposed to do that", 0.0f, 0, "sword");
        // take 20 damage, gain 20 coolant
        Ability humanoid3 = new Ability("Acid Spray", "Sizzle", 0.0f, 5, "sword");
        // lower enemy def by 5 for 3 turns

        // Chicken - Locomotion
        Ability chicken1 = new Ability("Falcon Dive", "Strike from the heavens", 0.2f, 35, "sword");
        // Decrease enemy coolant
        // BuffDebuff c1e = new BuffDebuff("Defense Up", new PlayerStats(0, 10, 0, 0, 0), 4);
        // bulk2.chicken1(c1e);

        // Legs - Locomotion
        Ability legs1 = new Ability("Jump Kick", "HIYAHH", 0.3f, 25, "thud");
        BuffDebuff l1e = new BuffDebuff("ATK Up", new PlayerStats(10, 0, 0, 0, 0), 3);
        legs1.AddEffect(l1e); // Take 10 damage
        Ability legs2 = new Ability("Roundhouse", "The world destroying kick", 0.1f, 35, "thud");
        BuffDebuff l2e = new BuffDebuff("RGN Up", new PlayerStats(0, 0, 0, 0, 20), 2);
        legs2.AddEffect(l2e);
        Ability legs3 = new Ability("Flying Knee", "Take that", 0.3f, 20, "thud");
        // Take 5 damage

        // Spider - Locomotion
        Ability spider1 = new Ability("Evasive Maneuvers", "Dodge and weave", 0.0f, 5, "sword");
        BuffDebuff s1e = new BuffDebuff("DEF Up", new PlayerStats(0, 15, 0, 0, 0), 2);
        spider1.AddEffect(s1e);
        // heals, decrease enemy coolant

        // Wheel - Locomotion
        Ability wheel1 = new Ability("Smash and Grab", "Bang 'n snatch", 0.2f, 50, "thud");
        // enemey -10 cool, you +10 cool
        Ability wheel2 = new Ability("Ooze Tar", "Blub blub", 0.3f, 15, "thud");
        // deal damage to enemy
        Ability wheel3 = new Ability("Overclock Assault", "Fully engaged", 0.4f, 30, "thud");
        BuffDebuff w13 = new BuffDebuff("ATK Up", new PlayerStats(10, 0, 0, 0, 0), 2);
        wheel3.AddEffect(w13);
        // take 20 damage






        // Pew - Equipment
        Ability pew1 = new Ability("Rapid Fire", "Pew Pew Pew Pew", 0.2f, 5, "gun");
        Ability pew2 = new Ability("High Explosive Shell", "Kaboom", 0.4f, 75, "gun");
        // Take 30 damage
        Ability pew3 = new Ability("Decoy Flare", "Can't touch this", 0.0f, 5, "gun");
        // Decrease enemy atk by 10 for 2 turns


        // Pow - Equipment
        Ability pow1 = new Ability("Smash", "Smashy smashy", 0.3f, 15, "thud");
        // Take 5 damage
        Ability pow2 = new Ability("Rapid Strike", "Smashy smashy", 0.45f, 40, "thud");
        // heals



        // Slash - Equipment
        Ability slash1 = new Ability("Parry", "The best offense is a good defense", 0.15f, 20, "sword");
        BuffDebuff ssh1e = new BuffDebuff("Defense Up", new PlayerStats(0, 10, 0, 0, 0), 2);
        slash1.AddEffect(ssh1e);
        Ability slash2 = new Ability("Chop Chop", "Slice 'em and Dice 'em", 0.4f, 25, "sword");
        Ability slash3 = new Ability("Target Wounds", "Aim for the knee", 0.2f, 20, "sword");
        // Lower enemy defense by 10 for 3 turns








        // Chassis
        chassis = new List<Hardware>();
        Hardware c1 = new Hardware(1, 3, "Bulky", "Tough and durable.", Hardware.Category.CHASSIS, new PlayerStats(0, 20, 20, 0, 0));
        c1.addAbility(bulk1);
        c1.addAbility(bulk2);
        Hardware c2 = new Hardware(2, 3, "Focused", "Excellent cooling.", Hardware.Category.CHASSIS, new PlayerStats(10, 10, 0, 20, 5));
        c2.addAbility(focused1);
        c2.addAbility(focused2);
        c2.addAbility(focused3);
        Hardware c3 = new Hardware(3, 3, "Humanoid", "Well-balanced.", Hardware.Category.CHASSIS, new PlayerStats(20, 0, 10, 10, 2));
        c3.addAbility(humanoid1);
        // c3.addAbility(humanoid2);
        c3.addAbility(humanoid3);

        chassis.Add(c1);
        chassis.Add(c2);
        chassis.Add(c3);


        // Locomotion
        locomotion = new List<Hardware>();
        Hardware l1 = new Hardware(1, 3, "Spider", "Highly creepy.", Hardware.Category.LOCOMOTION, new PlayerStats(10, 10, 10, 10, 10));
        l1.addAbility(spider1);
        Hardware l2 = new Hardware(4, 3, "Wheel", "Great for flat terrain.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 20, 20, 10, 0));
        l2.addAbility(wheel1);
        l2.addAbility(wheel2);
        l2.addAbility(wheel3);
        Hardware l3 = new Hardware(3, 3, "Chicken", "Bawk!", Hardware.Category.LOCOMOTION, new PlayerStats(30, 0, 0, 10, 10));
        l3.addAbility(chicken1);
        Hardware l4 = new Hardware(2, 3, "Legs", "Standard android legs.", Hardware.Category.LOCOMOTION, new PlayerStats(0, 10, 0, 30, 20));
        l4.addAbility(legs1);
        l4.addAbility(legs2);
        l4.addAbility(legs3);

        locomotion.Add(l1);
        locomotion.Add(l2);
        locomotion.Add(l3);
        locomotion.Add(l4);

        // Equipment
        equipment = new List<Hardware>();
        Hardware e1 = new Hardware(1, 3, "Slash", "A tough saber.", Hardware.Category.EQUIPMENT, new PlayerStats(20, 20, 0, 0, 0));
        e1.addAbility(slash1);
        e1.addAbility(slash2);
        e1.addAbility(slash3);
        Hardware e2 = new Hardware(2, 3, "Pew", "High-powered rifle.", Hardware.Category.EQUIPMENT, new PlayerStats(30, 0, 0, 30, 0));
        e2.addAbility(pew1);
        e2.addAbility(pew2);
        e2.addAbility(pew3);
        Hardware e3 = new Hardware(3, 3, "Pow", "Blunt crushing instrument.", Hardware.Category.EQUIPMENT, new PlayerStats(10, 20, 10, 10, 10));
        e3.addAbility(pow1);
        e3.addAbility(pow2);

        equipment.Add(e1);
        equipment.Add(e2);
        equipment.Add(e3);

        // All
        allHardware = new List<Hardware>();
        allHardware.AddRange(chassis);
        allHardware.AddRange(locomotion);
        allHardware.AddRange(equipment);

        //placeholder
        // foreach (Hardware h in allHardware)
        // {
        //     string sound;
        //     switch (h.name)
        //     {
        //         case "Slash":
        //         case "Chicken": sound = "sword"; break;
        //         case "Pew": sound = "gun"; break;
        //         default: sound = "thud"; break;
        //     }

        //     Ability a = new Ability(h.name + " basic attaque", "smash em with ur " + h.name + "!", Random.Range(0.1f, 1), Random.Range(10, 100), sound);

        //     if (Random.Range(0, 3) == 2)
        //     {
        //         a.AddEffect("3-turn Atk up, but less regen", new PlayerStats(10, 0, 0, 0, -5), 3);
        //     }

        //     h.addAbility(a);
        // }

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
