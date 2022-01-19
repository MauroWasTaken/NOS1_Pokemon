using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move
{
    public int id;
    public string Name { get; protected set; } = "10-000-000-volt-Thunderbolt";
    public int Power { get; protected set; } = 192;
    public int Pp { get; protected set; } = 1;
    public int MaxPp { get; protected set; } = 5;
    public int Accuracy { get; protected set; }
    public string DamageClass { get; protected set; } = "special";
    public string Ailment { get; protected set; } = "none";
    public int AilmentChance { get; protected set; } = 0;
    public int RecoilAmount { get; protected set; } = 0;
    public Type Type { get; protected set; }
    public Move(int id)
    {

    }
    public void Use()
    {
        Pp-=1;
    }
}
