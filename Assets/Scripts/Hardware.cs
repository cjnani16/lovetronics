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


    public Hardware(int i, string n, string d, Category c, PlayerStats s, string path)
    {
        id = i;
        name = n+" "+c.ToString();
        description = d;
        category = c;
        statBoosts = s;
        sprite = Resources.Load<Sprite>(path);
        abilities = new List<Ability>();
    }

    public Hardware(int i, string n, string d, Category c, PlayerStats s, Sprite sp)
    {
        id = i;
        name = n+" "+c.ToString();
        description = d;
        category = c;
        statBoosts = s;
        sprite = sp;
        abilities = new List<Ability>();
    }

    public Hardware rollSimilar() 
    {
        float modi = Random.Range(0.5f,1.5f);
        string prefix = "";
        if (modi>1.45) {
            prefix = "Godlike";
        } else if (modi>1.2) {
            prefix = "Enhanced";
        } else if (modi < 0.8) {
            prefix = "Flawed";
        } else if (modi < 0.6) {
            prefix = "Ruined";
        }

        Hardware hw = new Hardware(id, prefix+" "+name.Split(' ')[0], description, category, statBoosts*modi, sprite);
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