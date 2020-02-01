using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private int attack;
    private int defense;
    private int maxHealth;
    private int maxCoolant;
    private int coolantRegen;

    public PlayerStats(int atk, int def, int hp, int cool, int regen)
    {
        attack = atk;
        defense = def;
        maxHealth = hp;
        maxCoolant = cool;
        coolantRegen = regen;
    }

    public int getAttack()
    {
        return attack;
    }
    public int getDefense()
    {
        return defense;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public int getMaxCoolant()
    {
        return maxCoolant;
    }
    public int getCoolantRegen()
    {
        return coolantRegen;
    }


}