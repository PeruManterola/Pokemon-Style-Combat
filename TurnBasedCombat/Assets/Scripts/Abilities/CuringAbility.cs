using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Healing Ability", order = 3)]
public class CuringAbility : AbilityObject
{
    private Ability ability;
    public override void ExecuteAbility(Pokemon attacker, Pokemon target)
    {
        int hitChance = Random.Range(0, 101);
        if (hitChance <= accuracy)
        {
            attacker.Heal(power);
            Debug.Log($"{attacker.pokemonName} se ha curado!");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} se ha curado!");
        }
        else
        {
            Debug.Log($"{attacker.pokemonName} ha fallado {abilityName}");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} ha fallado {abilityName}");
        }
    }
}
