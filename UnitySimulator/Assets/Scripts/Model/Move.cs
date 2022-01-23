using Newtonsoft.Json;

namespace Model
{
    public class Move
    {
        public Move()
        {
        }

        public Move(int id, string name, int power, int pp, int maxPp, int accuracy, string damageClass,
            int priority, string ailment, int ailmentChance, int recoilAmount, Type type)
        {
            Id = id;
            Name = name;
            Power = power;
            Pp = pp;
            MaxPp = maxPp;
            Accuracy = accuracy;
            DamageClass = damageClass;
            Priority = priority;
            Ailment = ailment;
            AilmentChance = ailmentChance;
            RecoilAmount = recoilAmount;
            Type = type;
        }
        
        [JsonConstructor]
        public Move(int id, string name, Type type, string damageClass, int pp, int power, int accuracy,
            int priority, string ailment, int ailmentChance, int recoilAmount)
        {
            Id = id;
            Name = name;
            Power = power;
            MaxPp = pp;
            Pp = pp;
            Accuracy = accuracy;
            DamageClass = damageClass;
            Priority = priority;
            Ailment = ailment;
            AilmentChance = ailmentChance;
            RecoilAmount = recoilAmount;
            Type = type;
        }

        public Move(int id)
        {
        }

        public int Id { get; }
        public string Name { get; }
        public int Power { get; }
        public int Pp { get; private set; }
        public int MaxPp { get; }
        public int Accuracy { get; }
        public string DamageClass { get; }
        public int Priority { get; }
        public string Ailment { get; }
        public int AilmentChance { get; }
        public int RecoilAmount { get; }
        public Type Type { get; }

        public void Use()
        {
            Pp -= 1;
        }
    }
}