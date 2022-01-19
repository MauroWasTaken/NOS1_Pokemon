using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    int dex = 26;
    string name;
    Dictionary<string, int> baseStats = new Dictionary<string, int>()
    {
        {"maxHp",90},
        {"hp",1},
        {"attack",90},
        {"defense",90},
        {"spAttack",90},
        {"spDefence",90},
        {"speed",90}
    };
    List<Move> availableMoves = new List<Move>();
    List<Move> selectedMoves = new List<Move>();
    //Pokemon from scratch
    public Pokemon(int dex)
    {
        this.dex = dex;
        this.name = dex + "";
    }
    //Pokemon from preset
    public Pokemon(int dex, List<Move> moves)
    {

    }
    private void SetupStats(int hp, int attack, int defense, int spAttack, int spDefence, int speed)
    {
        baseStats["maxHp"] = hp;
        baseStats["hp"] = hp;
        baseStats["attack"] = attack;
        baseStats["defense"] = defense;
        baseStats["spAttack"] = spAttack;
        baseStats["spDefence"] = spDefence;
        baseStats["speed"] = speed;
    }
    public void UseMove(Move move)
    {

    }
    public void TakeDamage(Move move)
    {

    }
    public int Dex { get { return dex; } }
    public string Name { get { return name; } }
    public Dictionary<string, int> BaseStats { get { return baseStats; } }
    public List<Move> AvailableMoves { get { return availableMoves; } }
    public List<Move> SelectedMoves { get { return selectedMoves; } }
}
