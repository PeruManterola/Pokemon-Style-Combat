using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void PokeUpdate();

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass
}

[System.Serializable]
public class Pokemon : IAction, ITakeDamage
{
    private int _hp;
    public int maxHp;
    public int attack;
    public int defense;
    public int speed;

    public PokemonType type1, type2;

    public Ability ability_1, ability_2, ability_3, ability_4;

    public Team<Pokemon> myTeam = new Team<Pokemon>();

    public string[] abilityNames = new string[4];

    public string pokemonName;
    public Sprite pokemonSprite;

    public PokeUpdate OnHpChange;

    public class PokemonHandler : EventArgs
    {
        public Pokemon pokemon;

        public PokemonHandler(Pokemon pokemon)
        {
            this.pokemon = pokemon;
        }
    }

    public Pokemon(PokemonObject pokemonObject, Team<Pokemon> team)
    {
        if (pokemonObject != null)
        {
            maxHp = pokemonObject.maxHp;
            if (pokemonObject.ability_1 != null)
                ability_1 = new Ability(pokemonObject.ability_1, this);
            if (pokemonObject.ability_2 != null)
                ability_2 = new Ability(pokemonObject.ability_2, this);
            if (pokemonObject.ability_3 != null)
                ability_3 = new Ability(pokemonObject.ability_3, this);
            if (pokemonObject.ability_4 != null)
                ability_4 = new Ability(pokemonObject.ability_4, this);
            this.pokemonName = pokemonObject.pokemonName;

            abilityNames[0] = ability_1?.abilityName;
            abilityNames[1] = ability_2?.abilityName;
            abilityNames[2] = ability_3?.abilityName;
            abilityNames[3] = ability_4?.abilityName;

            pokemonSprite = pokemonObject.pokemonSprite;

            attack = pokemonObject.attack;
            defense = pokemonObject.defense;
            speed = pokemonObject.speed;
            type1 = pokemonObject.type1;
            type2 = pokemonObject.type2;

            myTeam = team;

            Initialize();
        }
    }

    public int HP
    {
        get { return _hp; }
        set
        {
            _hp = Mathf.Clamp(value, 0, maxHp);
           
            OnHpChange?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        HP = _hp + amount;
    }

    public int EvaluateSpeed(int num)
    {
        int finalSpeed = speed;
        switch (num)
        {
            case 1:
                finalSpeed += ability_1.speed;
                break;
            case 2:
                finalSpeed += ability_2.speed;
                break;
            case 3:
                finalSpeed += ability_3.speed;
                break;
            case 4:
                finalSpeed += ability_4.speed;
                break;
            default:
                finalSpeed += ability_1.speed;
                break;
        }
        return finalSpeed;
    }

    public bool TakeDamage(Pokemon attacker, AbilityObject ability, out bool isCritical, out float effectiveness)
    {
        float critical = 1f;
        isCritical = false;
        if (UnityEngine.Random.value * 100f <= 6.25f)
        {
            critical = 2f;
            isCritical = true;
        }

        float type = TypeChart.GetEffectiveness(ability.type, this.type1) * TypeChart.GetEffectiveness(ability.type, this.type2);
        effectiveness = type;
        float modifiers = UnityEngine.Random.Range(0.85f, 1f) * type * critical;
        float resultingDamage = ability.power * (attacker.attack) / defense;
        int damageToDeal = Mathf.FloorToInt(resultingDamage * modifiers);

        HP -= damageToDeal;
        if (HP == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Initialize()
    {
        HP = maxHp;
        OnHpChange?.Invoke();
    }

    public virtual void UseAbility(Pokemon attacker, Pokemon target, Ability ability)
    {
        ability.execute(this, target);
    }


    public class TypeChart
    {
        static float[][] chart =
        {
            //                      Nor Fire Water Ele Grass
            /*Nor*/     new float[] {1f, 1f,  1f,   1f,  1f},
            /*Fire*/    new float[] {1f, 0.5f, 0.5f, 1f, 2f},
            /*Water*/   new float[] {1f, 2f, 0.5f, 2f, 0.5f},
            /*Ele*/     new float[] {1f, 1f, 2f, 0.5f, 0.5f},
            /*Grass*/   new float[] {1f, 0.5f, 2f, 2f, 0.5f}

        };

        public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
        {
            if (attackType == PokemonType.None || defenseType == PokemonType.None)
                return 1;

            int row = (int)attackType - 1;
            int col = (int)defenseType - 1;

            return chart[row][col];
        }
    }
}
