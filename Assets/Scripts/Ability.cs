using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDebuff
{
    public bool isHealBleed;
    public float healBleedAmt;
    public string name;
    private PlayerStats statChanges;
    private int duration;

    public BuffDebuff(string n, PlayerStats s, int d)
    {
        this.name = n;
        this.statChanges = s;
        this.duration = d;
        this.isHealBleed = false;
    }

    //heal or bleed (dot or instant)
    public BuffDebuff(string n, float amount, int d)
    {
        this.name = n;
        this.statChanges = new PlayerStats(0, 0, 0, 0, 0);
        this.healBleedAmt = amount;
        this.isHealBleed = true;
        this.duration = d;
    }

    public PlayerStats GetStatChanges()
    {
        return statChanges;
    }

    //lower timer and return true if expired.
    public void decrement()
    {
        duration--;
    }

    public bool isExpired()
    {
        return duration <= 0;
    }
}
public struct Ability
{
    public string name, description;
    public float power, cost;
    string sound;
    public List<BuffDebuff> appliedEffects;
    public List<BuffDebuff> enemyAppliedEffects;

    public Ability(string name, string description, float power, float cost, string sound)
    {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.appliedEffects = new List<BuffDebuff>();
        this.enemyAppliedEffects = new List<BuffDebuff>();
        this.sound = sound;
    }
    public void AddEffect(string name, PlayerStats statChange, int duration)
    {
        this.appliedEffects.Add(new BuffDebuff(name, statChange, duration));
        this.description += ("\n" + statChange.stringify());
    }

    public void AddEffect(BuffDebuff e)
    {
        this.appliedEffects.Add(e);
        this.description += ("\n" + e.GetStatChanges().stringify());
    }

    public void AddEnemyEffect(string name, PlayerStats statChange, int duration)
    {
        this.enemyAppliedEffects.Add(new BuffDebuff(name, statChange, duration));
        this.description += ("\nTo enemy:" + statChange.stringify());
    }

    public void AddEnemyEffect(BuffDebuff e)
    {
        this.enemyAppliedEffects.Add(e);
        this.description += ("\nTo enemy:" + e.GetStatChanges().stringify());
    }

    //for something more complex than a stat buff/debuff
    public void ApplyOnCastSpecialEffect(ref BattlerState user, ref BattlerState target)
    {
        return;
    }
    public float damageCalc(ref BattlerState user, ref BattlerState target)
    {
        return Mathf.Round(this.power * user.getAlteredStats().getAttack() * (100f / (target.getAlteredStats().getDefense() + 100f)));
    }

    public void playSound()
    {
        if (this.sound != "")
        {
            string sound = this.sound + Random.Range(1, 3);
            Debug.Log(sound);
            AudioManager.instance.Play(sound);
        }
    }
}