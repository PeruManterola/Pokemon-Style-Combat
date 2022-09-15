using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pokemon", menuName = "Pokemon/New Pokemon", order = 1)]
public class PokemonObject : ScriptableObject
{
    public string pokemonName;
    public Sprite pokemonSprite;
    public int maxHp;
    public int attack;
    public int defense;
    public int speed = 50;

    public PokemonType type1, type2;


    public AbilityObject ability_1, ability_2, ability_3, ability_4;
}
