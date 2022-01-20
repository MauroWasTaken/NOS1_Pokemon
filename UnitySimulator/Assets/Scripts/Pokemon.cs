using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon
{
    public int Dex { get; protected set; } = 26;
    public string Name { get; protected set; }
    public Dictionary<string, int> BaseStats { get; protected set; } = new Dictionary<string, int>()
    {
        {"maxHp",90},
        {"hp",80},
        {"attack",90},
        {"defense",90},
        {"spAttack",90},
        {"spDefence",90},
        {"speed",90}
    };
    public List<Type> Types { get; protected set; } = new List<Type>();
    public List<Move> AvailableMoves { get; protected set; } = new List<Move>();
    public List<Move> SelectedMoves { get; protected set; } = new List<Move>();
    //Pokemon from scratch
    public Pokemon(int dex)
    {
        this.Dex = dex;
        this.Name = dex + "";
    }
    //Pokemon from preset
    public Pokemon(int dex, List<Move> moves)
    {

    }
    private void SetupStats(int hp, int attack, int defense, int spAttack, int spDefence, int speed)
    {
        BaseStats["maxHp"] = hp;
        BaseStats["hp"] = hp;
        BaseStats["attack"] = attack;
        BaseStats["defense"] = defense;
        BaseStats["spAttack"] = spAttack;
        BaseStats["spDefence"] = spDefence;
        BaseStats["speed"] = speed;
    }
    public void Attack(Move move, Pokemon target)
    {
        int damage = (int)Math.Floor((2 * move.Power * (this.BaseStats["attack"] / target.BaseStats["defense"]) / 50 + 2) * target.TypeEffectiveness(move));
        target.TakeDamage(damage);
    }
    public float TypeEffectiveness(Move move)
    {
        float effectiveness = 1;
        foreach (Type type in Types)
        {
            if (type.WeakAgainst.Contains(move.Type.name))
            {
                effectiveness=effectiveness*2;
            }
            if (type.StrongAgainst.Contains(move.Type.name))
            {
                effectiveness=effectiveness/2;
            }
            if (type.NoDamageFrom.Contains(move.Type.name))
            {
                effectiveness=0;
            }
        }
        return effectiveness;
    }
    public void TakeDamage(int amount)
    {
        BaseStats["hp"] -= amount;
        BattleManagerScript._instance.UpdateUi();
        if (BaseStats["hp"] <= 0)
        {
            BattleManagerScript._instance.EndMatch(this);
        }
    }
}
