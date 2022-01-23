using System;
using MongoDB.Bson;

namespace Model.Conversion
{
    public class MoveConverter
    {
        public static Move From(BsonDocument bson)
        {
            var move = new Move
            (
                bson.GetValue("id").AsInt32,
                bson.GetValue("name").AsString,
                GetNullableInt(bson, "power"),
                GetNullableInt(bson, "pp"),
                GetNullableInt(bson, "pp"),
                GetNullableInt(bson, "accuracy"),
                GetNullableString(bson, "damageClass"),
                GetNullableInt(bson, "priority"),
                GetNullableString(bson, "ailment"),
                GetNullableInt(bson, "ailmentChance"),
                GetNullableInt(bson, "recoilAmount"),
                TypeConverter.From(bson.GetValue("type").AsBsonDocument)
            );
            return move;
        }

        private static int GetNullableInt(BsonDocument bson, string value)
        {
            return IsNull(bson.GetValue(value)) ? int.MinValue : bson.GetValue(value).AsInt32;
        }

        private static string GetNullableString(BsonDocument bson, string value)
        {
            return IsNull(bson.GetValue(value)) ? string.Empty : bson.GetValue(value).AsString;
        }

        private static bool IsNull(BsonValue bsonValue) => bsonValue.BsonType == BsonType.Null;
    }
}