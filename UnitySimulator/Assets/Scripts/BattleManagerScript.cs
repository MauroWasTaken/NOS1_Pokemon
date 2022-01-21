using System.Collections.Generic;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    public static BattleManagerScript _instance;
    [SerializeField] GameObject playerName;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerHpSlider;
    [SerializeField] GameObject playerHpLabel;
    [SerializeField] GameObject enemyName;
    [SerializeField] GameObject enemySprite;
    [SerializeField] GameObject enemyHpSlider;
    [SerializeField] List<GameObject> moveLabels;
    Pokemon enemyPokemon;
    List<Move> moves = new List<Move>();
    Pokemon playerPokemon;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            playerPokemon = Database.Instance.FindPokemonBy(59);
            StartGame();
        }
    }

    private void StartGame()
    {
        GenerateEnemyPokemon();
        LoadUi();
    }

    private void LoadUi()
    {
        LoadSprites();
        playerName.GetComponent<TextMeshProUGUI>().SetText(playerPokemon.Name);
        enemyName.GetComponent<TextMeshProUGUI>().SetText(enemyPokemon.Name);
        UpdateHp(true);
        UpdateHp(false);
    }

    private void UpdateHp(bool player)
    {
        Pokemon pokemon = new Pokemon(0);
        GameObject slider = new GameObject();
        if (player)
        {
            playerHpLabel.GetComponent<TextMeshProUGUI>()
                .SetText(playerPokemon.BaseStats.Hp + " / " + playerPokemon.BaseStats.MaxHp);
            pokemon = playerPokemon;
            slider = playerHpSlider;
        }
        else
        {
            pokemon = enemyPokemon;
            slider = enemyHpSlider;
        }

        slider.GetComponent<Image>().fillAmount = (1f * pokemon.BaseStats.Hp / pokemon.BaseStats.MaxHp);
    }

    private void GenerateEnemyPokemon()
    {
        enemyPokemon = new Pokemon(25);
    }

    private void LoadSprites()
    {
        Sprite loadedSprite = Resources.Load<Sprite>("Pokemon/Front/" + enemyPokemon.Dex);
        enemySprite.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        playerSprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Back/" + playerPokemon.Dex);
    }

    private void LoadMoves()
    {
        Sprite loadedSprite = Resources.Load<Sprite>("Pokemon/Front/" + enemyPokemon.Dex);
        enemySprite.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        playerSprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Back/" + playerPokemon.Dex);
    }

    public void MoveClicked(int index)
    {
    }
}