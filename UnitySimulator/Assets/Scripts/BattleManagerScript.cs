using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    Pokemon playerPokemon;
    [SerializeField] GameObject playerName;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerHpSlider;
    [SerializeField] GameObject playerHpLabel;
    Pokemon enemyPokemon;
    [SerializeField] GameObject enemyName;
    [SerializeField] GameObject enemySprite;
    [SerializeField] GameObject enemyHpSlider;
    [SerializeField] List<GameObject> moveLabels;
    List<Move> moves = new List<Move>();
    public static BattleManagerScript _instance;
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            playerPokemon = new Pokemon(6);
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
            playerHpLabel.GetComponent<TextMeshProUGUI>().SetText(playerPokemon.BaseStats["hp"] + " / " + playerPokemon.BaseStats["maxHp"]);
            pokemon = playerPokemon;
            slider = playerHpSlider;
        }
        else
        {
            pokemon = enemyPokemon;
            slider = enemyHpSlider;
        }
        slider.GetComponent<Image>().fillAmount = (1f * pokemon.BaseStats["hp"] / pokemon.BaseStats["maxHp"]);
    }
    private void GenerateEnemyPokemon()
    {
        enemyPokemon = new Pokemon(700);
    }
    private void LoadSprites()
    {
        Sprite loadedSprite = Resources.Load<Sprite>("Pokemon/Front/" + enemyPokemon.Dex);
        enemySprite.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokemon/Back/" + playerPokemon.Dex);
    }
    private void LoadMoves()
    {
        Sprite loadedSprite = Resources.Load<Sprite>("Pokemon/Front/" + enemyPokemon.Dex);
        enemySprite.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokemon/Back/" + playerPokemon.Dex);
    }
    public void MoveClicked(int index)
    {

    }
}
