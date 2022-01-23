using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Model
{
    public class Pokemon
    {
        public Pokemon()
        {
        }

        /// <summary>
        ///     Instantiate a new pokemon from scratch.
        /// </summary>
        /// <param name="dex">The pokedex number.</param>
        public Pokemon(int dex)
        {
            Dex = dex;
            Name = dex + "";
        }

        /// <summary>
        ///     Instantiate a new pokemon from preset.
        /// </summary>
        /// <param name="dex">The pokedex number.</param>
        /// <param name="moves">A list of move the pokemon can learn.</param>
        public Pokemon(int dex, List<Move> moves)
        {
        }

        public Pokemon(int dex, string name, BaseStats baseStats, List<Type> types, List<Move> availableMoves)
        {
            Dex = dex;
            Name = name;
            BaseStats = baseStats;
            Types = types;
            AvailableMoves = availableMoves;
        }

        /// <summary>
        ///     Instantiate from a preset
        /// </summary>
        /// <param name="_id"></param>
        /// <param name="dex"></param>
        /// <param name="name"></param>
        /// <param name="selectedMoves"></param>
        /// <param name="types"></param>
        /// <param name="baseStats"></param>
        [JsonConstructor]
        public Pokemon(string _id, int dex, string name, List<Move> selectedMoves, List<Type> types,
            BaseStats baseStats)
        {
            Id = _id;
            Dex = dex;
            Name = name;
            SelectedMoves = selectedMoves;
            BaseStats = baseStats;
            Types = types;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int Dex { get; set; }
        public string Name { get; set; }
        public BaseStats BaseStats { get; set; }
        public List<Type> Types { get; set; }
        [JsonIgnore] public List<Move> AvailableMoves { get; set; } = new List<Move>();
        [JsonProperty("moves")] public List<Move> SelectedMoves { get; set; } = new List<Move>();

        public void Attack(Move move, Pokemon target)
        {
            var damage = 0;

            if (move.Power > 0)
            {
                const int levelDmg = 2 * 100 / 5 + 2;
                int statsDmg = move.DamageClass == "physical"
                    ? GetRealStat(BaseStats.Attack) / GetRealStat(target.BaseStats.Defense)
                    : GetRealStat(BaseStats.SpAttack) / GetRealStat(target.BaseStats.SpDefense);
                int baseDmg = levelDmg * move.Power * statsDmg / 50 + 2;

                damage = (int)Math.Floor(baseDmg * target.TypeEffectiveness(move));
            }

            move.Use();
            target.TakeDamage(damage);
        }
        public int GetDamage(Move move, Pokemon target){
            var damage = 0;
            if (move.Power > 0)
            {
                const int levelDmg = 2 * 100 / 5 + 2;
                int statsDmg = move.DamageClass == "physical"
                    ? GetRealStat(BaseStats.Attack) / GetRealStat(target.BaseStats.Defense)
                    : GetRealStat(BaseStats.SpAttack) / GetRealStat(target.BaseStats.SpDefense);
                int baseDmg = levelDmg * move.Power * statsDmg / 50 + 2;

                damage = (int)Math.Floor(baseDmg * target.TypeEffectiveness(move));
            }
            return damage;
        }

        private static int GetRealStat(int stat) => (int)(Math.Floor(0.01 * (2 * stat) * 100) + 5);

        private float TypeEffectiveness(Move move)
        {
            float effectiveness = 1;
            foreach (Type type in Types)
            {
                if (type.WeakAgainst.Contains(move.Type.Name))
                    effectiveness *= 2;
                else if (type.StrongAgainst.Contains(move.Type.Name))
                    effectiveness /= 2;
                else if (type.NoDamageFrom.Contains(move.Type.Name))
                    effectiveness = 0;
            }

            return effectiveness;
        }

        private void TakeDamage(int amount)
        {
            if (amount > BaseStats.Hp)
                BaseStats.Hp = 0;
            else
                BaseStats.Hp -= amount;
        }
    }
}