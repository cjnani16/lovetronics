using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRobotConfig
{
    List<Ability> getAbilities();

    PlayerStats getStatBoosts();
}
