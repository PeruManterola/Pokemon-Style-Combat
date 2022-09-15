using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    void UseAbility(Pokemon attacker, Pokemon target, Ability ability);
}
