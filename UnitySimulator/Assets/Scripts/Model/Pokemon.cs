using System;
using System.Collections.Generic;

namespace Model
{
    public class Pokemon
    {
        //Pokemon from scratch
        public Pokemon(int dex)
        {
            Dex = dex;
            Name = dex + "";
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

        public int Dex { get; set; }
        public string Name { get; set; }
        public BaseStats BaseStats { get; set; }
        public List<Type> Types { get; set; }
        public List<Move> AvailableMoves { get; set; }
        public List<Move> SelectedMoves { get; set; } = new List<Move>();

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