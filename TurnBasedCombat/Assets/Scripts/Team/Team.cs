using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team<T> where T : Pokemon
{

    private Node selectedPokemon;

    public Pokemon SelectedPokemon { get { return selectedPokemon.data; } }
    public int SelectedPosition { get{ return selectedPokemon.position; } }

    private Node pokemon_1, pokemon_2, pokemon_3, pokemon_4, pokemon_5, pokemon_6;
    private int pokemonCount = 0;
    public int livePokemon = 0;

    public int Size { get { return pokemonCount; } }


    public void AddPokemon(T entry)
    {
        pokemonCount++;
        livePokemon++;
        if (pokemonCount < 6)
        {
            switch (pokemonCount)
            {
                case 1:
                    pokemon_1 = new Node(entry,1);
                    if (selectedPokemon == null)
                        selectedPokemon = pokemon_1;
                    break;
                case 2:
                    pokemon_2 = new Node(entry,2);
                    break;
                case 3:
                    pokemon_3 = new Node(entry,3);
                    break;
                case 4:
                    pokemon_4 = new Node(entry,4);
                    break;
                case 5:
                    pokemon_4 = new Node(entry,5);
                    break;
                case 6:
                    pokemon_6 = new Node(entry,6);
                    break;
                default:
                    pokemon_1 = new Node(entry,1);
                    if (selectedPokemon == null)
                        selectedPokemon = pokemon_1;
                    break;
            }
        }
        else
        {
            pokemonCount--;
            livePokemon--;
            Debug.LogError("Team is full");
        }
    }

    public Pokemon SelectPokemon(int position)
    {
        switch (position)
        {
            case 1:
                selectedPokemon = pokemon_1;
                break;
            case 2:
                selectedPokemon = pokemon_2;
                break;
            case 3:
                selectedPokemon = pokemon_3;
                break;
            case 4:
                selectedPokemon = pokemon_4;
                break;
            case 5:
                selectedPokemon = pokemon_5;
                break;
            case 6:
                selectedPokemon = pokemon_6;
                break;
            default:
                selectedPokemon = pokemon_1;
                break;
        }
        return selectedPokemon.data;
    }

    public Pokemon GetPokemon(int position)
    {
        Pokemon pokeInfo;
        switch (position)
        {
            case 1:
                pokeInfo = pokemon_1?.data;
                break;
            case 2:
                pokeInfo = pokemon_2?.data;
                break;
            case 3:
                pokeInfo = pokemon_3?.data;
                break;
            case 4:
                pokeInfo = pokemon_4?.data;
                break;
            case 5:
                pokeInfo = pokemon_5?.data;
                break;
            case 6:
                pokeInfo = pokemon_6?.data;
                break;
            default:
                pokeInfo = pokemon_1?.data;
                break;
        }
        return pokeInfo;
    }

    public void RemovePokemon(int position)
    {
        switch (position)
        {
            case 1:
                pokemon_1 = null;
                break;
            case 2:
                pokemon_2 = null;
                break;
            case 3:
                pokemon_3 = null;
                break;
            case 4:
                pokemon_4 = null;
                break;
            case 5:
                pokemon_4 = null;
                break;
            case 6:
                pokemon_6 = null;
                break;
            default:
                pokemon_1 = null;
                break;
        }
        pokemonCount--;
        livePokemon--;
    }

    public void EmptyTeam()
    {
        pokemon_1 = null;
        pokemon_2 = null;
        pokemon_3 = null;
        pokemon_4 = null;
        pokemon_5 = null;
        pokemon_6 = null;

        pokemonCount = 0;
        livePokemon = 0;
    }

    public class Node
    {
        public T data;
        public int position;

        public Node(T data, int position)
        {
            this.data = data;
            this.position = position;
        }
    }
}
