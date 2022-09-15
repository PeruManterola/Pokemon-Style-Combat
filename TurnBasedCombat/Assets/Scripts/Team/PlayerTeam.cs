using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeam : TeamBase
{
    public override void Awake()
    {
        base.Awake();
        DisplayLogs();
    }
    public override void DisplayLogs()
    {
        Debug.Log($"Player team count: {team.Size}");
        Debug.Log($"Players selected pokemon: {team.SelectedPokemon.pokemonName}");
    }
}
