using System.Collections.Generic;
using System.Linq;
using Model;
using TMPro;
using UnityEngine;

public class MenuControllerScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject battleMenu;
    [SerializeField] GameObject endScreen;
    //pokemon selection variables
    [SerializeField] GameObject pokemonSelectionMenu;
    [SerializeField] GameObject presetDropdown;
    [SerializeField] GameObject presetSprite;
    [SerializeField] List<GameObject> presetsMoveLabels;
    static List<Pokemon> presets;
    //preset creator variables
    [SerializeField] GameObject myPresetsMenu;
    [SerializeField] GameObject pokemonDropdown;
    [SerializeField] GameObject movesDropdown;
    [SerializeField] GameObject pokemonNameInput;
    [SerializeField] GameObject pokemonSprite;
    [SerializeField] List<GameObject> pokemonMoveLabels;
    static List<Pokemon> pokemons;
    public void Start()
    {
        MainMenu();
        presets = new List<Pokemon>();
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
        pokemons = GetPokemons();
        LoadDropdown(pokemonDropdown, pokemons);
        LoadMoveDropdown(pokemons[0].AvailableMoves);
        pokemonNameInput.GetComponent<TMP_InputField>().text = pokemons[0].Name;
        ChangeSelectedPokemon(0);
    }

    public void OpenPokemonSelection()
    {
        DisableAll();
        pokemonSelectionMenu.SetActive(true);
        LoadPresets();
        LoadDropdown(presetDropdown, presets);
        ChangeSelectedPreset(0);
    }

    private void LoadDropdown(GameObject target, List<Pokemon> pokemons)
    {
        target.GetComponent<TMP_Dropdown>().options.Clear();
        foreach (Pokemon pokemon in pokemons)
        {
            target.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(pokemon.Name));
        }

        target.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = pokemons[0].Name;
    }
    private void LoadMoveDropdown(List<Move> moves)
    {
        movesDropdown.GetComponent<TMP_Dropdown>().options.Clear();
        foreach (Move move in moves)
        {
            movesDropdown.GetComponent<TMP_Dropdown>().options.Add(new TMP_Dropdown.OptionData(move.Name));
        }
        movesDropdown.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = moves[0].Name;
    }



    private List<Pokemon> GetPokemons()
    {
        List<Pokemon> output = new List<Pokemon>();
        List<Pokemon> pokemons = Database.Instance.FindAllPokemons();

        pokemons.ForEach(pokemon =>
        {
            output.Add(pokemon);
        });
        return output;
    }

    private void UpdateMoves(List<GameObject> targets, Pokemon pokemon)
    {
        for (var i = 0; i < pokemon.SelectedMoves.Count; i++)
        {
            targets[i].GetComponent<TextMeshProUGUI>().text = pokemon.SelectedMoves[i].Name;
            string message = $"PP: {pokemon.SelectedMoves[i].Pp}/{pokemon.SelectedMoves[i].MaxPp}\n" +
                             $"Type: {pokemon.SelectedMoves[i].Type.Name}\n" +
                             $"Class: {pokemon.SelectedMoves[i].DamageClass}\n" +
                             $"Power: {pokemon.SelectedMoves[i].Power}";
            targets[i].transform.parent.GetChild(1).gameObject.GetComponent<UseTooltipScript>().Message =
                message;
        }
        for (var i = pokemon.SelectedMoves.Count; i < 4; i++)
        {
            targets[i].GetComponent<TextMeshProUGUI>().text = " ";
            targets[i].transform.parent.GetChild(1).gameObject.GetComponent<UseTooltipScript>().Message = "no move selected";
        }
    }
    public void ChangeSelectedPreset(int useless)
    {
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        presetSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokemon/Front/" + presets[id].Dex);
        UpdateMoves(presetsMoveLabels, presets[id]);
    }
    public void ChangeSelectedPokemon(int useless)
    {
        int id = pokemonDropdown.GetComponent<TMP_Dropdown>().value;
        pokemonSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Pokemon/Front/" + pokemons[id].Dex);
        pokemons[id].SelectedMoves = new List<Move>() { pokemons[id].AvailableMoves[0] };
        UpdateMoves(pokemonMoveLabels, pokemons[id]);
        LoadMoveDropdown(pokemons[id].AvailableMoves);
        pokemonNameInput.GetComponent<TMP_InputField>().text = pokemons[id].Name;
    }
    public void AddMove(int useless)
    {
        int id = pokemonDropdown.GetComponent<TMP_Dropdown>().value;
        int moveId = movesDropdown.GetComponent<TMP_Dropdown>().value;
        if (4 > pokemons[id].SelectedMoves.Count())
        {
            pokemons[id].SelectedMoves.Add(pokemons[id].AvailableMoves[moveId]);
        }
        UpdateMoves(pokemonMoveLabels, pokemons[id]);
    }
    public void RemoveMove(int index)
    {
        int id = pokemonDropdown.GetComponent<TMP_Dropdown>().value;
        if (index < pokemons[id].SelectedMoves.Count())
        {
            pokemons[id].SelectedMoves.RemoveAt(index);
        }
        UpdateMoves(pokemonMoveLabels, pokemons[id]);
    }

    public void StartBattle()
    {
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
    private void LoadPresets()
    {
        presets = Database.Instance.FindAllPresets();
    }
    public void SavePreset()
    {
        int id = pokemonDropdown.GetComponent<TMP_Dropdown>().value;
        if (pokemons[id].SelectedMoves.Count() > 0)
        {
            pokemons[id].Name = pokemonNameInput.GetComponent<TMP_InputField>().text;
            Database.Instance.SavePreset(pokemons[id]);
            MainMenu();
        }
    }
    public void DeletePreset()
    {
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        presetDropdown.GetComponent<TMP_Dropdown>().value=0;
        Debug.Log(presets[id]);
        Database.Instance.DeletePresetBy(presets[id].Id);
        LoadPresets();
        LoadDropdown(presetDropdown, presets);
        ChangeSelectedPreset(0);
    }
}