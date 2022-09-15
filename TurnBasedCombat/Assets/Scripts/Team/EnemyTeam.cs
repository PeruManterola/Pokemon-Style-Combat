using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeam : TeamBase
{
    public override void Awake()
    {
        base.Awake();
        DisplayLogs();
    }
    public override void DisplayLogs()
    {
        Debug.Log($"Enemy team count: {team.Size}");
        Debug.Log($"Enemies selected pokemon: {team.SelectedPokemon.pokemonName}");
    }
}
