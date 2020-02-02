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
    public PlayerStats zeroMin() {
        attack = Mathf.Max(0,attack);
        defense = Mathf.Max(0,defense);
        maxHealth = Mathf.Max(1,maxHealth);
        maxCoolant = Mathf.Max(0,maxCoolant);
        coolantRegen = Mathf.Max(0,coolantRegen);
        return this;
    }

    public static PlayerStats operator +(PlayerStats a, PlayerStats b) {
        return new PlayerStats(
            a.getAttack()+b.getAttack(), 
            a.getDefense()+b.getDefense(),
            a.getMaxHealth() + b.getMaxHealth(),
            a.getMaxCoolant()+b.getMaxCoolant(),
            a.getCoolantRegen()+b.getCoolantRegen()
        );
    }

    public static PlayerStats operator *(PlayerStats a, float b) {
        return new PlayerStats(
            Mathf.RoundToInt(a.getAttack()*b), 
            Mathf.RoundToInt(a.getDefense()*b),
            Mathf.RoundToInt(a.getMaxHealth()*b),
            Mathf.RoundToInt(a.getMaxCoolant()*b),
            Mathf.RoundToInt(a.getCoolantRegen()*b)
        );
    }

    public string stringify() {
        List<string> s = new List<string>();

        if (this.getAttack()>0)
            s.Add("Attack "+this.getAttack()+"↑");
        else if (this.getAttack()<0)
            s.Add("Attack "+this.getAttack()+"↓");

        if (this.getDefense()>0)
            s.Add("Defense "+this.getDefense()+"↑");
        else if (this.getDefense()<0)
            s.Add("Defense "+this.getDefense()+"↓");

        if (this.getCoolantRegen()>0)
            s.Add("Regen "+this.getCoolantRegen()+"↑");
        else if (this.getCoolantRegen()<0)
            s.Add("Regen "+this.getCoolantRegen()+"↓");

        if (this.getMaxCoolant()>0)
            s.Add("Coolant "+this.getMaxCoolant()+"↑");
        else if (this.getMaxCoolant()<0)
            s.Add("Coolant "+this.getMaxCoolant()+"↓");

        if (this.getMaxHealth()>0)
            s.Add("HP "+this.getMaxHealth()+"↑");
        else if (this.getMaxHealth()<0)
            s.Add("HP "+this.getMaxHealth()+"↓");

        return System.String.Join(", ", s.ToArray());
    }

}