public class BaseStats
{
    public int MaxHp { get; }
    public int Hp { get; set; }
    public int Attack { get; }
    public int Defense { get; }
    public int SpAttack { get; }
    public int SpDefense { get; }
    public int Speed { get; }

    public BaseStats(int maxHp, int hp, int attack, int defense, int spAttack, int spDefense, int speed)
    {
        MaxHp = maxHp;
        Hp = hp;
        Attack = attack;
        Defense = defense;
        SpAttack = spAttack;
        SpDefense = spDefense;
        Speed = speed;
    }
}