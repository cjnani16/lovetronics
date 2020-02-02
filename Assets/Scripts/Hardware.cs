using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hardware
{
    public enum Category { CHASSIS, LOCOMOTION, EQUIPMENT };

    public int id;

    public string name;
    public Category category;

    public PlayerStats statBoosts;
    public Ability ability;


    public Hardware(int i, string n, Category c, PlayerStats s)
    {
        id = i;
        name = n;
        category = c;
        statBoosts = s;
    }

    public Ability getAbility()
    {
        return ability;
    }

    public PlayerStats getStatBoosts()
    {
        return statBoosts;
    }

    public Category getCategory()
    {
        return category;
    }
}