using Newtonsoft.Json;

public class BaseStats
{
    [JsonConstructor]
    public BaseStats(int hp, int attack, int defense, int spAttack, int spDefense, int speed)
    {
        Hp = hp;
        MaxHp = hp;
        Attack = attack;
        Defense = defense;
        SpAttack = spAttack;
        SpDefense = spDefense;
        Speed = speed;
    }

    public int MaxHp { get; set; }

    public int Hp { get; set; }

    public int Attack { get; }

    public int Defense { get; }

    public int SpAttack { get; }

    public int SpDefense { get; }

    public int Speed { get; }
}