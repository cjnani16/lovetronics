using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff
{
    public string name;
    private PlayerStats statChanges;
    private int duration;
    public BuffDebuff(string n, PlayerStats s, int d) {
        this.name = n;
        this.statChanges = s;
        this.duration = d;
    }

    public PlayerStats GetStatChanges() {
        return statChanges;
    }

    //lower timer and return true if expired.
    public void decrement() {
        duration--;
    }

    public bool isExpired() {
        return duration<=0;
    }

    public string stringify() {
        string s = "";

        if (statChanges.getAttack()>0)
            s+="Attack ↑, ";
        else if (statChanges.getAttack()<0)
            s+="Attack ↓, ";

        if (statChanges.getDefense()>0)
            s+="Defense ↑, ";
        else if (statChanges.getDefense()<0)
            s+="Defense ↓, ";

        if (statChanges.getCoolantRegen()>0)
            s+="Regen ↑, ";
        else if (statChanges.getCoolantRegen()<0)
            s+="Regen ↓";

        if (statChanges.getMaxCoolant()>0)
            s+="Coolant ↑, ";
        else if (statChanges.getMaxCoolant()<0)
            s+="Coolant ↓, ";

        if (statChanges.getMaxHealth()>0)
            s+="HP ↑, ";
        else if (statChanges.getMaxHealth()<0)
            s+="HP ↓, ";

        return s;
    }
}
public struct Ability
{
    public string name, description;
    public float power, cost;
    public List<BuffDebuff> appliedEffects;

    public Ability(string name, string description, float power, float cost) {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.appliedEffects = new List<BuffDebuff>();
    }
    public void AddEffect(string name, PlayerStats statChange, int duration) {
        this.appliedEffects.Add(new BuffDebuff(name, statChange, duration));
    }

    //for something more complex than a stat buff/debuff
    public void ApplyOnCastSpecialEffect(ref BattlerState user, ref BattlerState target) {
        return;
    }
    public float damageCalc(ref BattlerState user, ref BattlerState target) {
        return Mathf.Round(this.power*user.getAlteredStats().getAttack()*(100f/(target.getAlteredStats().getDefense()+100f)));
    }
}