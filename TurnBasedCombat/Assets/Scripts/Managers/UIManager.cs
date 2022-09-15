using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private Team<Pokemon> playerTeam = new Team<Pokemon>();
    private Team<Pokemon> enemyTeam = new Team<Pokemon>();

    [Space(10)]
    public Button ability1, ability2, ability3, ability4;
    [Space(10)]
    public Image playerPokemonImg, enemyPokemonImg;
    public TextMeshProUGUI playerName, enemyName;
    public TextMeshProUGUI playerPS;
    public TextMeshProUGUI infoDisplayText;
    [Space(10)]
    public Slider pHealth, eHealth;
    [Space(10)]
    public Image pFill, eFill;
    [Space(20)]
    public GameObject playerUI;
    public GameObject abilities;
    public GameObject changeButton;
    public GameObject endScreen;

    private TextMeshProUGUI ability1_text, ability2_text, ability3_text, ability4_text;

    [Space(10)]
    public Button pokemon_1, pokemon_2, pokemon_3, pokemon_4, pokemon_5, pokemon_6;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        GetReferences();
        UpdateUi();

        playerTeam.SelectedPokemon.OnHpChange += UpdateHealthBar;
        enemyTeam.SelectedPokemon.OnHpChange += UpdateHealthBar;
        CombatManager.instance.OnAbilityUse += RefreshAbilityDisplay;
        CombatManager.instance.OnPokemonChangeEnd += UpdateUi;
        CombatManager.instance.OnPokemonChangeStart += Unsubscribe;
    }

    public void UpdateUi()
    {
        RefreshAbilityDisplay();
        RefreshImages();
        SetupHealthBars();
        RefreshTeam();
        UpdateHealthBar();
        RefreshNames();

        playerTeam.SelectedPokemon.OnHpChange += UpdateHealthBar;
        enemyTeam.SelectedPokemon.OnHpChange += UpdateHealthBar;
    }

    public void Unsubscribe()
    {
        playerTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
        enemyTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
    }

    private void SetupHealthBars()
    {
        pHealth.maxValue = playerTeam.SelectedPokemon.maxHp;
        pHealth.value = playerTeam.SelectedPokemon.HP;

        eHealth.maxValue = enemyTeam.SelectedPokemon.maxHp;
        eHealth.value = enemyTeam.SelectedPokemon.HP;
    }

    private void UpdateHealthBar()
    {
        pHealth.value = playerTeam.SelectedPokemon.HP;
        if (playerTeam.SelectedPokemon.HP == 0)
        {
            pFill.color = new Color(255, 0, 0, 0);
        }
        else
        {
            pFill.color = new Color(255, 0, 0, 255);
        }
        eHealth.value = enemyTeam.SelectedPokemon.HP;
        if (enemyTeam.SelectedPokemon.HP == 0)
        {
            eFill.color = new Color(255, 0, 0, 0);
        }
        else
        {
            eFill.color = new Color(255, 0, 0, 255);
        }

        playerPS.text = $"{playerTeam.SelectedPokemon.HP}/{playerTeam.SelectedPokemon.maxHp}";
    }

    private void GetReferences()
    {
        ability1_text = ability1.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        ability2_text = ability2.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        ability3_text = ability3.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        ability4_text = ability4.gameObject.GetComponentInChildren<TextMeshProUGUI>();

        playerTeam = FindObjectOfType<PlayerTeam>().team;
        enemyTeam = FindObjectOfType<EnemyTeam>().team;
    }

    public void RefreshAbilityDisplay()
    {
        ability1_text.text = playerTeam.SelectedPokemon?.ability_1?.abilityName;
        ability2_text.text = playerTeam.SelectedPokemon?.ability_2?.abilityName;
        ability3_text.text = playerTeam.SelectedPokemon?.ability_3?.abilityName;
        ability4_text.text = playerTeam.SelectedPokemon?.ability_4?.abilityName;
        if (playerTeam.SelectedPokemon.ability_1 != null)
            ability1_text.text += $" PP: {playerTeam.SelectedPokemon.ability_1.pp}";
        if (playerTeam.SelectedPokemon.ability_2 != null)
            ability2_text.text += $" PP: {playerTeam.SelectedPokemon.ability_2.pp}";
        if (playerTeam.SelectedPokemon.ability_3 != null)
            ability3_text.text += $" PP: {playerTeam.SelectedPokemon.ability_3.pp}";
        if (playerTeam.SelectedPokemon.ability_4 != null)
            ability4_text.text += $" PP: {playerTeam.SelectedPokemon.ability_4.pp}";

        if (playerTeam.SelectedPokemon.ability_1 == null)
            ability1.interactable = false;
        if (playerTeam.SelectedPokemon.ability_2 == null)
            ability2.interactable = false;
        if (playerTeam.SelectedPokemon.ability_3 == null)
            ability3.interactable = false;
        if (playerTeam.SelectedPokemon.ability_4 == null)
            ability4.interactable = false;
    }

    private void RefreshNames()
    {
        playerName.text = playerTeam.SelectedPokemon.pokemonName;
        enemyName.text = enemyTeam.SelectedPokemon.pokemonName;
    }

    private void RefreshTeam()
    {
        pokemon_1.GetComponent<Image>().sprite = playerTeam.GetPokemon(1)?.pokemonSprite;
        pokemon_2.GetComponent<Image>().sprite = playerTeam.GetPokemon(2)?.pokemonSprite;
        pokemon_3.GetComponent<Image>().sprite = playerTeam.GetPokemon(3)?.pokemonSprite;
        pokemon_4.GetComponent<Image>().sprite = playerTeam.GetPokemon(4)?.pokemonSprite;
        pokemon_5.GetComponent<Image>().sprite = playerTeam.GetPokemon(5)?.pokemonSprite;
        pokemon_6.GetComponent<Image>().sprite = playerTeam.GetPokemon(6)?.pokemonSprite;

        if (playerTeam.GetPokemon(1) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(1) || playerTeam.GetPokemon(1).HP == 0)
        {
            pokemon_1.interactable = false;
        }
        else
        {
            pokemon_1.interactable = true;
        }
        if (playerTeam.GetPokemon(2) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(2) || playerTeam.GetPokemon(2).HP == 0)
        {
            pokemon_2.interactable = false;
        }
        else
        {
            pokemon_2.interactable = true;
        }
        if (playerTeam.GetPokemon(3) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(3) || playerTeam.GetPokemon(3).HP == 0)
        {
            pokemon_3.interactable = false;
        }
        else
        {
            pokemon_3.interactable = true;
        }
        if (playerTeam.GetPokemon(4) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(4) || playerTeam.GetPokemon(4).HP == 0)
        {
            pokemon_4.interactable = false;
        }
        else
        {
            pokemon_4.interactable = true;
        }
        if (playerTeam.GetPokemon(5) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(5) || playerTeam.GetPokemon(5).HP == 0)
        {
            pokemon_5.interactable = false;
        }
        else
        {
            pokemon_5.interactable = true;
        }
        if (playerTeam.GetPokemon(6) == null || playerTeam.SelectedPokemon == playerTeam.GetPokemon(6) || playerTeam.GetPokemon(6).HP == 0)
        {
            pokemon_6.interactable = false;
        }
        else
        {
            pokemon_6.interactable = true;
        }
    }

    public void RefreshImages()
    {
        playerPokemonImg.sprite = playerTeam.SelectedPokemon.pokemonSprite;
        enemyPokemonImg.sprite = enemyTeam.SelectedPokemon.pokemonSprite;
    }
    public void AbilitiesVisibility()
    {
        playerUI.SetActive(true);
        if (abilities.activeSelf)
        {
            abilities.SetActive(false);
            changeButton.SetActive(true);

        }
        else
        {
            abilities.SetActive(true);
            changeButton.SetActive(false);

        }
    }

    public void DisplayMessage(string message, bool critical, float effectivenes)
    {
        if (critical)
            StartCoroutine(DisplayIEnumerator(message));
           // infoDisplayText.text = "Ha sido un golpe crítico!!";

        switch (effectivenes)
        {
            case 0.5f:
                StartCoroutine(DisplayIEnumerator("Es poco eficaz."));
                break;
            case 1f:
                StartCoroutine(DisplayIEnumerator(message));
                break;
            case 2f:
                StartCoroutine(DisplayIEnumerator("Es muy eficaz!!"));
                break;
            default:
                StartCoroutine(DisplayIEnumerator(message));
                break;
        }
    }
    
    public void DisplayMessage(string message, float waitTime=1.5f)
    {
        StartCoroutine(DisplayIEnumerator(message,waitTime));
    }

    private IEnumerator DisplayIEnumerator(string toDisplay, float time=1.5f)
    {
        infoDisplayText.text = toDisplay;
        yield return new WaitForSeconds(time);
        infoDisplayText.text = string.Empty;
    }

    public void EndGame()
    {
        endScreen.SetActive(true);
    }

    private void OnDisable()
    {
        playerTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
        enemyTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
        CombatManager.instance.OnAbilityUse -= RefreshAbilityDisplay;
        CombatManager.instance.OnPokemonChangeEnd -= UpdateUi;
        CombatManager.instance.OnPokemonChangeStart -= Unsubscribe;
    }

    private void OnDestroy()
    {
        playerTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
        enemyTeam.SelectedPokemon.OnHpChange -= UpdateHealthBar;
        CombatManager.instance.OnAbilityUse -= RefreshAbilityDisplay;
        CombatManager.instance.OnPokemonChangeEnd -= UpdateUi;
        CombatManager.instance.OnPokemonChangeStart -= Unsubscribe;
    }
}
