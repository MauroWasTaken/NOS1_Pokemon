using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour
{
    public static BattleManagerScript Instance;
    [SerializeField] private GameObject menuControlleur;
    [SerializeField] private GameObject playerName;
    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject playerHpSlider;
    [SerializeField] private GameObject playerHpLabel;
    [SerializeField] private GameObject enemyName;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] private GameObject enemyHpSlider;
    [SerializeField] private List<GameObject> moveLabels;
    private Pokemon _enemyPokemon;
    private Pokemon _playerPokemon;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public void StartGame(Pokemon playerPokemon)
    {
        _playerPokemon = playerPokemon;
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
        playerName.GetComponent<TextMeshProUGUI>().SetText(_playerPokemon.Name);
        enemyName.GetComponent<TextMeshProUGUI>().SetText(_enemyPokemon.Name);
        UpdateHp(true);
        UpdateHp(false);
        UpdateMoves();
    }

    private void UpdateMoves()
    {
        for (var i = 0; i < _playerPokemon.SelectedMoves.Count; i++)
        {
            moveLabels[i].GetComponent<TextMeshProUGUI>().text = _playerPokemon.SelectedMoves[i].Name;
            string message = $"PP: {_playerPokemon.SelectedMoves[i].Pp}/{_playerPokemon.SelectedMoves[i].MaxPp}\n" +
                             $"Type: {_playerPokemon.SelectedMoves[i].Type.Name}\n" +
                             $"Class: {_playerPokemon.SelectedMoves[i].DamageClass}";
            moveLabels[i].transform.parent.GetChild(1).gameObject.GetComponent<UseTooltipScript>().Message = message;
        }
    }

    private void UpdateHp(bool player)
    {
        Pokemon pokemon;
        GameObject slider;
        if (player)
        {
            playerHpLabel.GetComponent<TextMeshProUGUI>()
                .SetText(_playerPokemon.BaseStats.Hp + " / " + _playerPokemon.BaseStats.MaxHp);
            pokemon = _playerPokemon;
            slider = playerHpSlider;
        }
        else
        {
            pokemon = _enemyPokemon;
            slider = enemyHpSlider;
        }

        slider.GetComponent<Image>().fillAmount = 1f * pokemon.BaseStats.Hp / pokemon.BaseStats.MaxHp;
    }

    private void GenerateEnemyPokemon()
    {
        _enemyPokemon = Database.Instance.FindPokemonBy(25);
        Move move = Database.Instance.FindMoveBy("tackle");
        _enemyPokemon.SelectedMoves.AddRange(Enumerable.Repeat(move, 4).ToList());
    }

    private void LoadSprites()
    {
        enemySprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Front/" + _enemyPokemon.Dex);
        playerSprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Back/" + _playerPokemon.Dex);
    }

    private void LoadMoves()
    {
        var loadedSprite = Resources.Load<Sprite>("Pokemon/Front/" + _enemyPokemon.Dex);
        enemySprite.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        string path = "Pokemon/Back/" + _playerPokemon.Dex;
        playerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
    }

    public void MoveClicked(int index)
    {
        if (_playerPokemon.SelectedMoves[index].Pp > 0)
            StartCoroutine(AttackSequence(index));
        // TODO : Else, play error Sound
    }

    private IEnumerator AttackSequence(int playerMove)
    {
        ToggleButtons();
        Pokemon fasterPokemon, slowerPokemon;
        int fastPokemonMove, slowPokemonMove;
        if (_playerPokemon.BaseStats.Speed > _enemyPokemon.BaseStats.Speed)
        {
            fasterPokemon = _playerPokemon;
            fastPokemonMove = playerMove;
            slowerPokemon = _enemyPokemon;
            slowPokemonMove = Random.Range(0, 3);
        }
        else if (_playerPokemon.BaseStats.Speed < _enemyPokemon.BaseStats.Speed)
        {
            fasterPokemon = _enemyPokemon;
            fastPokemonMove = Random.Range(0, 3);
            slowerPokemon = _playerPokemon;
            slowPokemonMove = playerMove;
        }
        else if (Random.value >= 0.5)
        {
            fasterPokemon = _playerPokemon;
            fastPokemonMove = playerMove;
            slowerPokemon = _enemyPokemon;
            slowPokemonMove = Random.Range(0, 3);
        }
        else
        {
            fasterPokemon = _enemyPokemon;
            fastPokemonMove = Random.Range(0, 3);
            slowerPokemon = _playerPokemon;
            slowPokemonMove = playerMove;
        }

        fasterPokemon.Attack(fasterPokemon.SelectedMoves[fastPokemonMove], slowerPokemon);
        Instance.UpdateUi();
        yield return new WaitForSeconds(1);
        if (slowerPokemon.BaseStats.Hp > 0)
        {
            slowerPokemon.Attack(slowerPokemon.SelectedMoves[slowPokemonMove], fasterPokemon);
            Instance.UpdateUi();
            yield return new WaitForSeconds(1);
        }

        CheckWinner();
        ToggleButtons();
    }

    private void ToggleButtons()
    {
        for (var i = 0; i < 4; i++)
            moveLabels[i].transform.parent.gameObject.SetActive(!moveLabels[i].transform.parent.gameObject.activeSelf);

        TooltipScript._instance.HideTooltip();
    }

    private void CheckWinner()
    {
        if (_enemyPokemon.BaseStats.Hp == 0)
            //player won
            menuControlleur.GetComponent<MenuControllerScript>().EndBattle(_playerPokemon, "Victory!");
        else if (_playerPokemon.BaseStats.Hp == 0)
            //player lost lol
            menuControlleur.GetComponent<MenuControllerScript>().EndBattle(_enemyPokemon, "Defeat!");
    }
}