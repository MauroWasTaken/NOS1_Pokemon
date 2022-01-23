using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;

namespace Model.Conversion
{
    public class TypeConverter
    {
        public static Type From(BsonDocument bson)
        {
            return new Type
            (
                bson.GetValue("name").AsString,
                BsonArrayToString(bson.GetValue("noDamageTo")),
                BsonArrayToString(bson.GetValue("noDamageFrom")),
                BsonArrayToString(bson.GetValue("superEffectiveTo")),
                BsonArrayToString(bson.GetValue("notVeryEffectiveTo")),
                BsonArrayToString(bson.GetValue("strongAgainst")),
                BsonArrayToString(bson.GetValue("weakAgainst"))
            );
        }

        private static List<string> BsonArrayToString(BsonValue bsonValue)
        {
            return bsonValue.AsBsonArray.Select(x => x.AsString).ToList();
        }
    }
}