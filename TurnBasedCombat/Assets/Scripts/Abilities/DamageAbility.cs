using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Damage Ability", order = 1)]
public class DamageAbility : AbilityObject
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
            Debug.Log($"{target.pokemonName} ha recibido daño, vida restante: {target.HP}");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} ha golpeado a {target.pokemonName} con {abilityName}.", critical, effectivenes);
        }
        else
        {
            Debug.Log($"{attacker.pokemonName} ha fallado {abilityName}");
            UIManager.instance.DisplayMessage($"{attacker.pokemonName} ha fallado {abilityName}");
        }
    }
}
