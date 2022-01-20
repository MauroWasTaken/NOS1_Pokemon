using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Model
{
    public class Pokemon
    {
        public int Dex { get; set; }

        public string Name { get; set; }

        public BaseStats BaseStats { get; set; }

        public List<Type> Types { get; set; }

        public List<Move> AvailableMoves { get; set; }

        public List<Move> SelectedMoves { get; set; }

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

        public Pokemon(int dex, string name, BaseStats baseStats, List<Type> types, List<Move> availableMoves)
        {
            Dex = dex;
            Name = name;
            BaseStats = baseStats;
            Types = types;
            AvailableMoves = availableMoves;
        }

        public void Attack(Move move, Pokemon target)
        {
            //int damage = (int) Math.Round((2*move.Power*(this.BaseStats["attack"]/target.BaseStats["defense"]))/50+2)*target.TypeEffectiveness(move);
        }

        public decimal TypeEffectiveness(Move move)
        {
            return 1;
        }

        public void TakeDamage(int amount)
        {
        }
    }
}