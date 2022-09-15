using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/DamageAndHealing Ability", order = 4)]
public class DamageHealingAbility : AbilityObject
{

    private Ability ability;
    public override void ExecuteAbility(Pokemon attacker, Pokemon target)
    {
        int hitChance = Random.Range(0, 101);
        if (hitChance <= accuracy)
        {
            bool critical;
            float effectivenes;
            target.TakeDamage(attacker, this, out critical, out effectivenes);
            attacker.Heal(power/2);
            Debug.Log($"{attacker.pokemonName} se ha curado!");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} se ha curado y {target.abilityNames} a recibido daño!");
        }
        else
        {
            Debug.Log($"{attacker.pokemonName} ha fallado {abilityName}");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} ha fallado {abilityName}");
        }
    }
}
