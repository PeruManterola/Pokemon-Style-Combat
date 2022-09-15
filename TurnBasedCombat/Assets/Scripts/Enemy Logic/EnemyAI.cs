using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public static EnemyAI instance;
    private EnemyTeam enemyTeam;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        enemyTeam = GetComponent<EnemyTeam>();
    }

    public bool InitialDecision()
    {
    
        int currentPs = enemyTeam.team.SelectedPokemon.HP;
        int maxPs = enemyTeam.team.SelectedPokemon.maxHp;

        bool lowHealth = false;
        bool change = false;
         
        if (currentPs <= maxPs * 0.2f)
        {
            lowHealth = true;
        }

        int r = Random.Range(0, 101);

        if (lowHealth && r <= 40) 
        {
            change = true;
        }

        return change;
    }

    public int EvaluateValues()
    {
        Pokemon selectedPokemon = enemyTeam.team.SelectedPokemon;
        int ability_1_value = 200;
        int ability_2_value = 200;
        int ability_3_value = 200;
        int ability_4_value = 200;

        if (selectedPokemon.ability_1 != null) { ability_1_value = selectedPokemon.ability_1.value; }

        if (selectedPokemon.ability_2 != null) { ability_2_value = selectedPokemon.ability_2.value; }

        if (selectedPokemon.ability_3 != null) { ability_3_value = selectedPokemon.ability_3.value; }

        if (selectedPokemon.ability_4 != null) { ability_4_value = selectedPokemon.ability_4.value; }

        int mostValuable = Mathf.Min(ability_1_value, ability_2_value, ability_3_value, ability_4_value);

        int selection=1;

        if (mostValuable == ability_1_value)
        {
            selection = 1;
        }
        else if (mostValuable==ability_2_value)
        {
            selection = 2;
        }
        else if (mostValuable==ability_3_value)
        {
            selection = 3;
        }
        else if (mostValuable==ability_4_value)
        {
            selection = 4;
        }
        Debug.Log("Abilidad seleccionada: " + selection);
        return selection;
    }
}
