using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model.Conversion
{
    public static class PokemonConverter
    {
        public static Pokemon From(BsonDocument bson)
        {
            var pokemon = new Pokemon
            (
                bson.GetValue("dex").AsInt32,
                bson.GetValue("name").AsString,
                GetBaseStatsFrom(bson.GetValue("baseStats").AsBsonDocument),
                bson.GetValue("types")
                    .AsBsonArray
                    .Select(x => TypeConverter.From(x.AsBsonDocument))
                    .ToList(),
                GetMovesFrom(bson)
            );

            return pokemon;
        }

        private static BaseStats GetBaseStatsFrom(BsonDocument bson)
        {
            return new BaseStats
            (
                bson.GetValue("hp").AsInt32,
                bson.GetValue("attack").AsInt32,
                bson.GetValue("defense").AsInt32,
                bson.GetValue("spAttack").AsInt32,
                bson.GetValue("spDefense").AsInt32,
                bson.GetValue("speed").AsInt32
            );
        }

        private static List<Move> GetMovesFrom(BsonDocument bson)
        {
            List<string> movesName = bson.GetValue("moves").AsBsonArray.Select(x => x.AsString).ToList();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.In("name", movesName);
            List<Move> moves = Database.Instance.FindMoves(filter);

            return moves;
        }
    }
}