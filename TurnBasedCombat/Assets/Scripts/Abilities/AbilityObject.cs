using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AbilityObject : ScriptableObject
{
    public string abilityName;

    public PokemonType type = PokemonType.None;
    public int pp = 5;
    public int speed = 30;
    public int accuracy = 100;
    public int power = 1;
    public virtual void ExecuteAbility(Pokemon Attacker, Pokemon target)
    {
        Debug.Log("Action done");
    }
}
