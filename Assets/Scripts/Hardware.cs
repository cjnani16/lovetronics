using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hardware : Monobehaviour, IRobotConfig
{
    private PlayerStats statBoosts;
    private List<Ability> abilities;

    public List<Ability> getAbilities()
    {
        return abilities;
    }

    public void getStatBoosts()
    {
        return statBoosts;
    }
}