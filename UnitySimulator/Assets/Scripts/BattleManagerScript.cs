using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    static Pokemon playerPokemon;
    [SerializeField] GameObject playerName;
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject playerHpSlider;
    [SerializeField] GameObject playerHpLabel;
    static Pokemon enemyPokemon;
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
            playerPokemon.SelectedMoves.Add(new Move(0));
            playerPokemon.SelectedMoves.Add(new Move(0));
            playerPokemon.SelectedMoves.Add(new Move(0));
            playerPokemon.SelectedMoves.Add(new Move(0));
            StartGame();
        }
    }
    private void StartGame()
    {
        GenerateEnemyPokemon();
        LoadUi();
    }
    public void UpdateUi()
    {
        UpdateHp(true);
        UpdateHp(false);
        UpdateMoves();
    }
    private void LoadUi()
    {
        LoadSprites();
        playerName.GetComponent<TextMeshProUGUI>().SetText(playerPokemon.Name);
        enemyName.GetComponent<TextMeshProUGUI>().SetText(enemyPokemon.Name);
        UpdateHp(true);
        UpdateHp(false);
        UpdateMoves();

    }
    private void UpdateMoves()
    {
        for (int i = 0; i < playerPokemon.SelectedMoves.Count; i++)
        {
            moveLabels[i].GetComponent<TextMeshProUGUI>().text = playerPokemon.SelectedMoves[i].Name;
        }
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
        enemyPokemon = new Pokemon(25);
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
    public static void MoveClicked(int index)
    {
        playerPokemon.Attack(playerPokemon.SelectedMoves[index], enemyPokemon);
    }
    public void EndMatch(Pokemon pokemon)
    {
        if (pokemon == playerPokemon)
        {
            //player won
        }
        else
        {
            //player lost lol
        }
    }
}
