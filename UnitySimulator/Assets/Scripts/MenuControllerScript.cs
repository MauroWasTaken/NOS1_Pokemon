using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuControllerScript : MonoBehaviour
{
    [SerializeField]
    List<Sprite> pokemonSprites;
    [SerializeField]
    GameObject presetDropdown;
    [SerializeField]
    GameObject presetImage;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject pokemonSelectionMenu;
    [SerializeField]
    GameObject myPresetsMenu;
    [SerializeField]
    GameObject battleMenu;
    public void Start()
    {
        MainMenu();
    }
    public void OpenPokemonSelection()
    {
        DisableAll();
        pokemonSelectionMenu.SetActive(true);
    }
    public void OpenMyPresets()
    {
        DisableAll();
        myPresetsMenu.SetActive(true);
    }
    public void MainMenu()
    {
        DisableAll();
        mainMenu.SetActive(true);
    }
    public void DisableAll()
    {
        mainMenu.SetActive(false);
        pokemonSelectionMenu.SetActive(false);
        myPresetsMenu.SetActive(false);
        battleMenu.SetActive(false);
    }
    public void ChangePokemonSprite(int useless)
    {
        int id = presetDropdown.GetComponent<TMP_Dropdown>().value;
        presetImage.GetComponent<Image>().sprite=pokemonSprites[id];
    }   
    public void StartBattle()
    {
        DisableAll();
        battleMenu.SetActive(true);
    }  
}
