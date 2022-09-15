using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Defence Ability", order = 2)]
public class DefenceAbility : AbilityObject
{
    private Ability ability;
    public override void ExecuteAbility(Pokemon attacker, Pokemon target)
    {
        int hitChance = Random.Range(0, 101);
        if (hitChance <= accuracy)
        {
            target.defense -= power;
            Debug.Log($"{target.pokemonName} ha perdido defensa");
            UIManager.instance.DisplayMessage($"{target.pokemonName} ha perdido defensa.");
        }
        else
        {
            Debug.Log($"{attacker.pokemonName} ha fallado {abilityName}");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} ha fallado {abilityName}");
        }
    }
}
