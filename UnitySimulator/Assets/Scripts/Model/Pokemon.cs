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
        [JsonIgnore] public List<Move> AvailableMoves { get; set; }
        [JsonProperty("moves")] public List<Move> SelectedMoves { get; set; }

        public void Attack(Move move, Pokemon target)
        {
            var damage =
                (int)Math.Floor((2 * move.Power * (BaseStats.Attack / target.BaseStats.Defense) / 50 + 2) *
                                target.TypeEffectiveness(move));
            move.Use();
            target.TakeDamage(damage);
        }

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