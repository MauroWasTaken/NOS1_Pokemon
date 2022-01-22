using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour
{
    [SerializeField] List<Sprite> pokemonSprites;
    [SerializeField] GameObject presetDropdown;
    [SerializeField] GameObject presetSprite;
    [SerializeField] List<GameObject> presetsMoveLabels;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject pokemonSelectionMenu;
    [SerializeField] GameObject myPresetsMenu;
    [SerializeField] GameObject battleMenu;
    [SerializeField] GameObject endScreen;
    List<Pokemon> presets;

    public void Start()
    {
        MainMenu();
    }

    public void DisableAll()
    {
        mainMenu.SetActive(false);
        pokemonSelectionMenu.SetActive(false);
        myPresetsMenu.SetActive(false);
        battleMenu.SetActive(false);
        endScreen.SetActive(false);
    }

    public void MainMenu()
    {
        DisableAll();
        mainMenu.SetActive(true);
    }

    public void OpenMyPresets()
    {
        DisableAll();
        myPresetsMenu.SetActive(true);
    }

    public void OpenPokemonSelection()
    {
        DisableAll();
        pokemonSelectionMenu.SetActive(true);
        loadPresets();
        loadDropdown();
        ChangeSelectedPokemon(0);
    }

    private void loadDropdown()
    {
        foreach (Pokemon pokemon in presets)
        {
            presetDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(pokemon.Name));
        }

        presetDropdown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = presets[0].Name;
    }

    private void loadPresets()
    {
        presets = new List<Pokemon>();
        List<Pokemon> pokemons = Database.Instance.FindAllPokemons();
        Move move = Database.Instance.FindMoveBy("tackle");

        pokemons.ForEach(pokemon =>
        {
            pokemon.SelectedMoves.AddRange(Enumerable.Repeat(move, 4).ToList());
            presets.Add(pokemon);
        });
    }

    private void UpdateMoves()
    {
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        for (var i = 0; i < presets[id].SelectedMoves.Count; i++)
        {
            presetsMoveLabels[i].GetComponent<TextMeshProUGUI>().text = presets[id].SelectedMoves[i].Name;
            string message = "PP: " + presets[id].SelectedMoves[i].MaxPp + "\n Type: " +
                             presets[id].SelectedMoves[i].Type?.Name + "\n Class: " +
                             presets[id].SelectedMoves[i].DamageClass;
            presetsMoveLabels[i].transform.parent.GetChild(1).gameObject.GetComponent<UseTooltipScript>().Message =
                message;
        }
    }

    public void ChangeSelectedPokemon(int useless)
    {
        loadPresets();
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        presetSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokemon/Front/" + presets[id].Dex);
        UpdateMoves();
    }

    public void StartBattle()
    {
        loadPresets();
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        DisableAll();
        battleMenu.SetActive(true);
        BattleManagerScript.Instance.StartGame(presets[id]);
    }

    public void EndBattle(Pokemon pokemon, string text)
    {
        DisableAll();
        endScreen.SetActive(true);
        endScreen.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite =
            Resources.Load<Sprite>("Pokemon/Front/" + pokemon.Dex);
        endScreen.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
    }
}