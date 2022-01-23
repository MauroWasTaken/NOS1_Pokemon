using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Model.Conversion
{
    public static class PresetConverter
    {
        public static BsonDocument From(Pokemon pokemon)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string presetSerialized = JsonConvert.SerializeObject(pokemon, serializerSettings);
            BsonDocument bsonDocument = BsonDocument.Parse(presetSerialized);
            
            return bsonDocument;
        }

        public static Pokemon From(BsonDocument bson)
        {
            object dotNetObj = BsonTypeMapper.MapToDotNetValue(bson);
            string objectInJson = JsonConvert.SerializeObject(dotNetObj);
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(objectInJson);
            
            return pokemon;
        }
    }
}