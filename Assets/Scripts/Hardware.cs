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
    public List<Ability> abilities;

    public Hardware(int i, string n, Category c, PlayerStats s)
    {
        id = i;
        name = n;
        category = c;
        statBoosts = s;
    }

    public List<Ability> getAbilities()
    {
        return abilities;
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