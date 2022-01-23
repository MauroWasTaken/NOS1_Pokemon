using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    [SerializeField] private GameObject battleLogs;
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
        StartCoroutine(battlePrep());
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
        int highestDamageMove = -1;
        int highestDamage = -1;
        for (var i = 0; i < _playerPokemon.SelectedMoves.Count; i++)
        {
            int moveDamage = _playerPokemon.GetDamage(_playerPokemon.SelectedMoves[i], _enemyPokemon);
            moveLabels[i].GetComponent<TextMeshProUGUI>().text = _playerPokemon.SelectedMoves[i].Name;
            if (moveDamage > highestDamage)
            {
                highestDamage = moveDamage;
                highestDamageMove = i;
            }
            string message = $"PP: {_playerPokemon.SelectedMoves[i].Pp}/{_playerPokemon.SelectedMoves[i].MaxPp}\n" +
                             $"Type: {_playerPokemon.SelectedMoves[i].Type.Name}\n" +
                             $"Class: {_playerPokemon.SelectedMoves[i].DamageClass}\n" +
                             $"Power: {_playerPokemon.SelectedMoves[i].Power}";
            moveLabels[i].transform.parent.GetChild(1).gameObject.GetComponent<UseTooltipScript>().Message = message;
        }
        moveLabels[highestDamageMove].GetComponent<TextMeshProUGUI>().text += "*";
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
    private IEnumerator battlePrep()
    {
        _enemyPokemon = Database.Instance.FindPokemonBy(Random.Range(1, 151));
        for (var i = 0; i < 4; i++)
        {
            int random = Random.Range(0, _enemyPokemon.AvailableMoves.Count - 1);
            Move enemyPokemonAvailableMove = _enemyPokemon.AvailableMoves[random];
            _enemyPokemon.SelectedMoves.Add(enemyPokemonAvailableMove);
            yield return new WaitForSeconds(0.005f);
        }
        _playerPokemon.BaseStats.Hp = (int)(Math.Floor(0.01 * (2 * _playerPokemon.BaseStats.Hp) * 100) + 100 + 10);
        _playerPokemon.BaseStats.MaxHp = _playerPokemon.BaseStats.Hp;
        _enemyPokemon.BaseStats.Hp = (int)(Math.Floor(0.01 * (2 * _enemyPokemon.BaseStats.Hp) * 100) + 100 + 10);
        _enemyPokemon.BaseStats.MaxHp = _enemyPokemon.BaseStats.Hp;
        LoadUi();
    }
    private void LoadSprites()
    {
        enemySprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Front/" + _enemyPokemon.Dex);
        playerSprite.GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Back/" + _playerPokemon.Dex);
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
        battleLogs.GetComponent<TextMeshProUGUI>().text = fasterPokemon.Name + " used " + fasterPokemon.SelectedMoves[fastPokemonMove].Name;
        Instance.UpdateUi();
        yield return new WaitForSeconds(1);
        if (slowerPokemon.BaseStats.Hp > 0)
        {
            slowerPokemon.Attack(slowerPokemon.SelectedMoves[slowPokemonMove], fasterPokemon);
            battleLogs.GetComponent<TextMeshProUGUI>().text = slowerPokemon.Name + " used " + slowerPokemon.SelectedMoves[slowPokemonMove].Name;
            Instance.UpdateUi();
            yield return new WaitForSeconds(1);
        }
        battleLogs.GetComponent<TextMeshProUGUI>().text = "";
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