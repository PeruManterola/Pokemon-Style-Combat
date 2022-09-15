using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TeamBase : MonoBehaviour
{
    public Team<Pokemon> team = new Team<Pokemon>();

    public List<PokemonObject> pokemons = new List<PokemonObject>();

    public virtual void Awake()
    {
        SetupTeam();
    }

    public virtual void DisplayLogs()
    {
        Debug.Log($"Team count: {team.Size}");
        Debug.Log($"Selected pokemon: {team.SelectedPokemon.pokemonName}");
    }

    public virtual void SetupTeam()
    {
        foreach (PokemonObject pokemon in pokemons)
        {
            if (pokemon != null)
                team.AddPokemon(new Pokemon(pokemon, team));
        }
    }

}
