using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hardware
{
    public enum Category { CHASSIS, LOCOMOTION, EQUIPMENT };

    public int id;
    public string name, description;
    public Category category;

    public PlayerStats statBoosts;

    public Sprite sprite;
    public List<Ability> abilities;
    public string soundName;
    int tier;


    public Hardware(int i, int t, string n, string d, Category c, PlayerStats s)
    {
        id = i;
        tier = t;
        name = n+" "+c.ToString();
        description = d;
        category = c;
        statBoosts = s;
        sprite = Resources.Load<Sprite>("Icons/"+System.Char.ToLower(c.ToString()[0])+id+"_"+tier);
        abilities = new List<Ability>();
    }

    public Hardware rollSimilar() 
    {
        float modi = Random.Range(0.5f,1.5f);
        string prefix = "";
        int tier = 3;
        if (modi>1.45) {
            prefix = "Godlike";
            tier = 5;
        } else if (modi>1.2) {
            prefix = "Enhanced";
            tier = 4;
        } else if (modi < 0.8) {
            prefix = "Flawed";
            tier = 2;
        } else if (modi < 0.6) {
            prefix = "Ruined";
            tier = 1;
        }

        Hardware hw = new Hardware(id, tier, prefix+" "+name.Split(' ')[0], description, category, statBoosts*modi);
        hw.abilities.AddRange(abilities);
        return hw;
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