namespace Model
{
    public class Move
    {
        public Move(int id, string name, int power, int pp, int maxPp, int accuracy, string damageClass,
            string ailment, int ailmentChance, int recoilAmount, Type type)
        {
            Id = id;
            Name = name;
            Power = power;
            Pp = pp;
            MaxPp = maxPp;
            Accuracy = accuracy;
            DamageClass = damageClass;
            Ailment = ailment;
            AilmentChance = ailmentChance;
            RecoilAmount = recoilAmount;
            Type = type;
        }

        public Move(int id)
        {
        }

        public int Id { get; }
        public string Name { get; protected set; }
        public int Power { get; protected set; }
        public int Pp { get; protected set; }
        public int MaxPp { get; protected set; }
        public int Accuracy { get; protected set; }
        public string DamageClass { get; protected set; }
        public string Ailment { get; protected set; }
        public int AilmentChance { get; protected set; }
        public int RecoilAmount { get; protected set; }
        public Type Type { get; protected set; }

        public void Use()
        {
            Pp -= 1;
        }
    }
}