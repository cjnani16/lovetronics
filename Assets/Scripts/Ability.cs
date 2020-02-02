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
}
public struct Ability
{
    public string name, description;
    public float power, cost;
    string sound;
    public List<BuffDebuff> appliedEffects;

    public Ability(string name, string description, float power, float cost, string sound) {
        this.name = name;
        this.description = description;
        this.power = power;
        this.cost = cost;
        this.appliedEffects = new List<BuffDebuff>();
        this.sound = sound;
    }
    public void AddEffect(string name, PlayerStats statChange, int duration) {
        this.appliedEffects.Add(new BuffDebuff(name, statChange, duration));
        this.description+= ("\n"+statChange.stringify());
    }

    public void AddEffect(BuffDebuff e) {
        this.appliedEffects.Add(e);
        this.description+= ("\n"+e.GetStatChanges().stringify());
    }

    //for something more complex than a stat buff/debuff
    public void ApplyOnCastSpecialEffect(ref BattlerState user, ref BattlerState target) {
        return;
    }
    public float damageCalc(ref BattlerState user, ref BattlerState target) {
        return Mathf.Round(this.power*user.getAlteredStats().getAttack()*(100f/(target.getAlteredStats().getDefense()+100f)));
    }

    public void playSound() {
        if (this.sound!="")
        AudioManager.instance.Play(this.sound+Random.Range(1,3));
    }
}