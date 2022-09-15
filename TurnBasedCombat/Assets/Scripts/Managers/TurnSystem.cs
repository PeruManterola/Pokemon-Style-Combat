using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public delegate void TurnChange();
//public De
public class TurnArgs : EventArgs
{
    public Pokemon playerPokemon;
    public Pokemon enemyPokemon;

    public TurnArgs(Pokemon player,Pokemon enemy)
    {
        playerPokemon = player;
        enemyPokemon = enemy;
    }
}

public class TurnSystem : MonoBehaviour
{
    public enum Turn
    {
        PickingAbility,
        PlayerTurn,
        EnemyTurn,
        ChangePokemon,
        TurnsEnded
    }

    public event TurnChange OnTurnChanged;
    public event EventHandler<TurnArgs> OnNewFight;

    public Turn currentTurn = Turn.PlayerTurn;

    private Team<Pokemon> playerTeam = new Team<Pokemon>();
    private Team<Pokemon> enemyTeam = new Team<Pokemon>();

    private int enemyPokemonNum = 1;

    private void Start()
    {
        playerTeam = FindObjectOfType<PlayerTeam>().team;
        enemyTeam = FindObjectOfType<EnemyTeam>().team;

        CombatManager.instance.OnAbilityUse += SwitchTurns;
        CombatManager.instance.OnPokemonChangeEnd += SwitchTurns;
        CombatManager.instance.OnPokemonChangeStart += SwitchTurns;
        CombatManager.instance.OnTargetDeath += HandleTurns;
        OnTurnChanged += HandleUi;
    }


    private void SwitchTurns()
    {
        if (currentTurn != Turn.ChangePokemon)
        {
            if (currentTurn == Turn.PlayerTurn)
            {
                currentTurn = Turn.EnemyTurn;
            }
            else
            {
                currentTurn = Turn.PlayerTurn;
            }
        }
        else
        {
            currentTurn = Turn.PlayerTurn;
            UIManager.instance.UpdateUi();
            UIManager.instance.AbilitiesVisibility();
            Debug.Log("Nueva pelea!!");
            OnNewFight?.Invoke(this,new TurnArgs(playerTeam.SelectedPokemon,enemyTeam.SelectedPokemon));
        }
        OnTurnChanged?.Invoke();
    }

    private void HandleUi()
    {
        if (currentTurn == Turn.PlayerTurn)
        {
            UIManager.instance.playerUI.SetActive(true);
        }
        else
        {
            UIManager.instance.playerUI.SetActive(false);
        }
    }

    private void HandleTurns()
    {
        playerTeam = FindObjectOfType<PlayerTeam>().team;
        enemyTeam = FindObjectOfType<EnemyTeam>().team;
        Debug.Log("Player pokemons remaining: " + playerTeam.livePokemon);
        Debug.Log("Enemy pokemons remaining: " + enemyTeam.livePokemon);
        if (playerTeam.livePokemon == 0 || enemyTeam.livePokemon == 0)
        {
            FinishBattle();
        }
        else if (playerTeam.SelectedPokemon.HP == 0)
        {
            currentTurn = Turn.ChangePokemon;
            UIManager.instance.AbilitiesVisibility();
            UIManager.instance.Unsubscribe();
            UIManager.instance.UpdateUi();

        }
        else if (enemyTeam.SelectedPokemon.HP == 0)
        {
            enemyPokemonNum++;
            currentTurn = Turn.ChangePokemon;
            UIManager.instance.AbilitiesVisibility();
            enemyTeam.SelectPokemon(enemyPokemonNum);
            UIManager.instance.DisplayMessage($"El {enemyTeam.SelectedPokemon.pokemonName} enemigo entra en combate!", 3f);
            //NewFight();
        }
    }

    public void NewFight()
    {
        UIManager.instance.Unsubscribe();
        currentTurn = Turn.PlayerTurn;
        UIManager.instance.UpdateUi();
        UIManager.instance.AbilitiesVisibility();
        CombatManager.instance.SetPokemons();
        Debug.Log("Nueva pelea!!");
        OnNewFight?.Invoke(this, new TurnArgs(playerTeam.SelectedPokemon, enemyTeam.SelectedPokemon));
    }

    public void KeepPokemon()
    {
        currentTurn = Turn.PlayerTurn;
        UIManager.instance.UpdateUi();
        UIManager.instance.AbilitiesVisibility();
        Debug.Log("Nueva pelea!!");
        OnNewFight?.Invoke(this, new TurnArgs(playerTeam.SelectedPokemon, enemyTeam.SelectedPokemon));
    }

    private void FinishBattle()
    {
        Debug.Log("COMBATE TERMINADO!!");
        currentTurn = Turn.TurnsEnded;
        UIManager.instance.playerUI.SetActive(false);
        UIManager.instance.EndGame();
    }

    public void ReloadFight()
    {
        SceneManager.LoadScene("PokeEscena");
    }

    private void OnDisable()
    {
        CombatManager.instance.OnAbilityUse -= SwitchTurns;
        CombatManager.instance.OnTargetDeath -= FinishBattle;
        CombatManager.instance.OnPokemonChangeEnd -= SwitchTurns;
        CombatManager.instance.OnPokemonChangeStart -= SwitchTurns;
        OnTurnChanged -= HandleUi;
    }

    private void OnDestroy()
    {
        CombatManager.instance.OnAbilityUse -= SwitchTurns;
        CombatManager.instance.OnTargetDeath -= FinishBattle;
        CombatManager.instance.OnPokemonChangeEnd -= SwitchTurns;
        CombatManager.instance.OnPokemonChangeStart -= SwitchTurns;
        OnTurnChanged -= HandleUi;
    }
}
