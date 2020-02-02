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

    public Sprite sprite;
    public List<Ability> abilities;


    public Hardware(int i, string n, Category c, PlayerStats s, string path)
    {
        id = i;
        name = n;
        category = c;
        statBoosts = s;
        sprite = Resources.Load<Sprite>(path);
        abilities = new List<Ability>();
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

    public void addAbility(Ability a)
    {
        this.abilities.Add(a);
    }
}