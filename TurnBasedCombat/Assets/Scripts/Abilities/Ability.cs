using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ExecuteAbility(Pokemon attacker, Pokemon target);

public class Ability
{
    public string abilityName;

    public PokemonType type = PokemonType.None;
    public int speed;
    public int accuracy = 100;
    public int power = 1;
    public int pp = 5;
    public Pokemon owner;

    public int value { get { return (speed + accuracy + power) / (3 + pp); } }

    public ExecuteAbility execute;

    public Ability(AbilityObject abilityObject, Pokemon owner)
    {
        abilityName = abilityObject.abilityName;
        pp = abilityObject.pp;
        type = abilityObject.type;
        execute = abilityObject.ExecuteAbility;
        accuracy = abilityObject.accuracy;
        speed = abilityObject.speed;
        this.owner = owner;
    }
}
