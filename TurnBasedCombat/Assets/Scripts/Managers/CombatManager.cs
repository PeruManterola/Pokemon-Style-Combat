using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;
    private TurnSystem turnSystem;

    public event PokeUpdate OnAbilityUse;
    public event PokeUpdate OnTargetDeath;
    public event PokeUpdate OnPokemonChangeStart;
    public event PokeUpdate OnPokemonChangeEnd;

    private Team<Pokemon> playerTeam = new Team<Pokemon>();
    private Team<Pokemon> enemyTeam = new Team<Pokemon>();
    private Pokemon playerPokemon;
    private Pokemon enemyPokemon;

    private int playerSpeed;
    private int enemySpeed;

    public int PlayerSpeed { get { return playerSpeed; } }
    public int EnemySpeed { get { return enemySpeed; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetTeams();
        turnSystem = FindObjectOfType<TurnSystem>();
        turnSystem.OnNewFight += SetPokemons;
    }

    private void SetTeams()
    {
        playerTeam = FindObjectOfType<PlayerTeam>().team;

        enemyTeam = FindObjectOfType<EnemyTeam>().team;

        SetPokemons();
    }

    public void SetPokemons(object sender, TurnArgs e)
    {
        playerPokemon = e.playerPokemon;
        enemyPokemon = e.enemyPokemon;
    }
    public void SetPokemons()
    {
        playerPokemon = playerTeam.SelectedPokemon;
        enemyPokemon = enemyTeam.SelectedPokemon;
    }

    public void ChangePokemon(int id)
    {
        if (turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon)
            OnPokemonChangeStart?.Invoke();
        switch (id)
        {
            case 1:
                playerPokemon = playerTeam.SelectPokemon(1);
                break;
            case 2:
                playerPokemon = playerTeam.SelectPokemon(2);
                break;
            case 3:
                playerPokemon = playerTeam.SelectPokemon(3);
                break;
            case 4:
                playerPokemon = playerTeam.SelectPokemon(4);
                break;
            case 5:
                playerPokemon = playerTeam.SelectPokemon(5);
                break;
            case 6:
                playerPokemon = playerTeam.SelectPokemon(6);
                break;
            default:
                playerPokemon = playerTeam.SelectPokemon(1);
                break;
        }
        UIManager.instance.DisplayMessage($"{playerTeam.SelectedPokemon.pokemonName} ha salido al combate.");

        if (turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon)
            OnPokemonChangeEnd?.Invoke();

        if (turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon)
        {
            EnemyUseAbility(EnemyAI.instance.EvaluateValues());
            StartCoroutine(OnlyEnemyCombat(enemyPokemon, playerPokemon, EnemyAI.instance.EvaluateValues()));
        }

        OnAbilityUse?.Invoke();
    }

    public void NewPokemon(int id)
    {
        OnPokemonChangeStart?.Invoke();
        switch (id)
        {
            case 1:
                playerPokemon = playerTeam.SelectPokemon(1);
                break;
            case 2:
                playerPokemon = playerTeam.SelectPokemon(2);
                break;
            case 3:
                playerPokemon = playerTeam.SelectPokemon(3);
                break;
            case 4:
                playerPokemon = playerTeam.SelectPokemon(4);
                break;
            case 5:
                playerPokemon = playerTeam.SelectPokemon(5);
                break;
            case 6:
                playerPokemon = playerTeam.SelectPokemon(6);
                break;
            default:
                playerPokemon = playerTeam.SelectPokemon(1);
                break;
        }
        OnPokemonChangeEnd?.Invoke();
    }

    public void PlayerUseAbility(int id)
    {
        bool changePokemon = EnemyAI.instance.InitialDecision();
        if (changePokemon && enemyTeam.livePokemon>1)
        {
            int selectedPokemon;
            do
            {
                selectedPokemon = Random.Range(1, enemyTeam.livePokemon + 1);
            } while (selectedPokemon == enemyTeam.SelectedPosition);

            Debug.Log("Enemigo cambiado!!"+" "+selectedPokemon);
            enemyPokemon = enemyTeam.SelectPokemon(selectedPokemon);
            if (turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon)
                OnPokemonChangeEnd?.Invoke();
        } 

        switch (id)
        {
            case 1:
                if (playerPokemon.ability_1 != null && playerPokemon.ability_1.pp > 0)
                {
                    playerSpeed = playerPokemon.EvaluateSpeed(1);
                    // playerPokemon.UseAbility(playerPokemon, enemyPokemon, playerPokemon.ability_1);
                    playerPokemon.ability_1.pp--;

                }
                break;
            case 2:
                if (playerPokemon.ability_2 != null && playerPokemon.ability_2.pp > 0)
                {
                    // playerPokemon.UseAbility(playerPokemon, enemyPokemon, playerPokemon.ability_2);
                    playerPokemon.ability_2.pp--;
                    playerSpeed = playerPokemon.EvaluateSpeed(2);
                }
                break;
            case 3:
                if (playerPokemon.ability_3 != null && playerPokemon.ability_3.pp > 0)
                {
                    // playerPokemon.UseAbility(playerPokemon, enemyPokemon, playerPokemon.ability_3);
                    playerPokemon.ability_3.pp--;
                    playerSpeed = playerPokemon.EvaluateSpeed(3);
                }
                break;
            case 4:
                if (playerPokemon.ability_4 != null && playerPokemon.ability_4.pp > 0)
                {
                    // playerPokemon.UseAbility(playerPokemon, enemyPokemon, playerPokemon.ability_4);
                    playerPokemon.ability_4.pp--;
                    playerSpeed = playerPokemon.EvaluateSpeed(4);
                }
                break;
            default:
                if (playerPokemon.ability_1 != null && playerPokemon.ability_1.pp > 0)
                {
                    // playerPokemon.UseAbility(playerPokemon, enemyPokemon, playerPokemon.ability_1);
                    playerPokemon.ability_1.pp--;
                    playerSpeed = playerPokemon.EvaluateSpeed(1);
                }
                break;
        }

        turnSystem.currentTurn = TurnSystem.Turn.PlayerTurn;
        int enemyId = 0;
        if (!changePokemon)
        {
            enemyId = EnemyAI.instance.EvaluateValues();
            EnemyUseAbility(enemyId);
        }
        if (!changePokemon)
        {
            HandleTurn(id, enemyId);
        }
        else
        {
            HandleTurn(id, enemyId,false);
        }
        

        if (enemyPokemon.HP == 0)
        {
            OnTargetDeath?.Invoke();
        }
    }

    private void EnemyUseAbility(int id)
    {
        switch (id)
        {
            case 1:
                if (enemyPokemon.ability_1 != null && enemyPokemon.ability_1.pp > 0)
                {
                    // enemyPokemon.UseAbility(enemyPokemon, playerPokemon, enemyPokemon.ability_1);
                    enemyPokemon.ability_1.pp--;
                    enemySpeed = enemyPokemon.EvaluateSpeed(1);
                }
                break;
            case 2:
                if (enemyPokemon.ability_2 != null && enemyPokemon.ability_2.pp > 0)
                {
                    //  enemyPokemon.UseAbility(enemyPokemon, playerPokemon, enemyPokemon.ability_2);
                    enemyPokemon.ability_2.pp--;
                    enemySpeed = enemyPokemon.EvaluateSpeed(2);
                }
                break;
            case 3:
                if (enemyPokemon.ability_3 != null && enemyPokemon.ability_3.pp > 0)
                {
                    // enemyPokemon.UseAbility(enemyPokemon, playerPokemon, enemyPokemon.ability_3);
                    enemyPokemon.ability_3.pp--;
                    enemySpeed = enemyPokemon.EvaluateSpeed(3);
                }
                break;
            case 4:
                if (enemyPokemon.ability_4 != null && enemyPokemon.ability_4.pp > 0)
                {
                    // enemyPokemon.UseAbility(enemyPokemon, playerPokemon, enemyPokemon.ability_4);
                    enemyPokemon.ability_4.pp--;
                    enemySpeed = enemyPokemon.EvaluateSpeed(4);
                }
                break;
            default:
                if (enemyPokemon.ability_1 != null && enemyPokemon.ability_1.pp > 0)
                {
                    // enemyPokemon.UseAbility(enemyPokemon, playerPokemon, enemyPokemon.ability_1);
                    enemyPokemon.ability_1.pp--;
                    enemySpeed = enemyPokemon.EvaluateSpeed(1);
                }
                break;
        }
        if (playerPokemon.HP == 0)
        {
            OnTargetDeath?.Invoke();
        }
    }

    private void ExecuteAbility(Pokemon attackingPokemon, Pokemon defendingPokemon, int id)
    {
        switch (id)
        {
            case 1:
                if (attackingPokemon.ability_1 != null && attackingPokemon.ability_1.pp > 0)
                {
                    attackingPokemon.UseAbility(attackingPokemon, defendingPokemon, attackingPokemon.ability_1);
                }
                break;
            case 2:
                if (attackingPokemon.ability_2 != null && attackingPokemon.ability_2.pp > 0)
                {
                    attackingPokemon.UseAbility(attackingPokemon, defendingPokemon, attackingPokemon.ability_2);
                }
                break;
            case 3:
                if (attackingPokemon.ability_3 != null && attackingPokemon.ability_3.pp > 0)
                {
                    attackingPokemon.UseAbility(attackingPokemon, defendingPokemon, attackingPokemon.ability_3);
                }
                break;
            case 4:
                if (attackingPokemon.ability_4 != null && attackingPokemon.ability_4.pp > 0)
                {
                    attackingPokemon.UseAbility(attackingPokemon, defendingPokemon, attackingPokemon.ability_4);
                }
                break;
            default:
                if (attackingPokemon.ability_1 != null && attackingPokemon.ability_1.pp > 0)
                {
                    attackingPokemon.UseAbility(attackingPokemon, defendingPokemon, attackingPokemon.ability_1);
                }
                break;
        }

        OnAbilityUse?.Invoke();
        if (defendingPokemon.HP == 0)
        {
            defendingPokemon.myTeam.livePokemon--;
            OnTargetDeath?.Invoke();
        }
    }

    public void HandleTurn(int playerId = 0, int enemyId = 0, bool enemyAttacks = true)
    {
        int enemyPokemonAbility = Random.Range(1, 5);
        if (PlayerSpeed >= EnemySpeed)
        {
            StartCoroutine(DoCombat(playerPokemon, enemyPokemon, playerId, enemyId,enemyAttacks));
        }
        else if(enemyAttacks)
        {
            StartCoroutine(DoCombat(enemyPokemon, playerPokemon, enemyId, playerId,enemyAttacks));
        }
    }

    private IEnumerator OnlyEnemyCombat(Pokemon attackingPokemon, Pokemon defendingPokemon, int id)
    {
        yield return new WaitForSeconds(2.5f);
        ExecuteAbility(attackingPokemon, defendingPokemon, id);
    }

    private IEnumerator DoCombat(Pokemon fastestPokemon, Pokemon slowestPokemon, int fastAbilityId, int slowAbilityId, bool slowestAttacks=true)
    {
        yield return new WaitForSeconds(1f);
        if (turnSystem.currentTurn != TurnSystem.Turn.TurnsEnded && turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon && turnSystem.currentTurn != TurnSystem.Turn.PickingAbility)
            ExecuteAbility(fastestPokemon, slowestPokemon, fastAbilityId);
        yield return new WaitForSeconds(2f);
        if (turnSystem.currentTurn != TurnSystem.Turn.TurnsEnded && turnSystem.currentTurn != TurnSystem.Turn.ChangePokemon && turnSystem.currentTurn != TurnSystem.Turn.PickingAbility && slowestAttacks)
            ExecuteAbility(slowestPokemon, fastestPokemon, slowAbilityId);

        if(!slowestAttacks)
            OnAbilityUse?.Invoke();
    }

    private void OnDestroy()
    {
        turnSystem.OnNewFight -= SetPokemons;
    }

    private void OnDisable()
    {
        turnSystem.OnNewFight -= SetPokemons;
    }
}
