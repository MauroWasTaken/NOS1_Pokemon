using System.Collections.Generic;
using System.Linq;
using Model.Conversion;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model
{
    public class Database
    {
        private const string DatabaseName = "pokesim";
        private static Database _instance;
        private readonly IMongoDatabase _connection;

        /// <summary>
        ///     Private constructor ton implement the singleton pattern.<br />
        ///     We don't want the game to use multiple instance of the connection.<br />
        ///     It will connect to the database the first time the accessor <see cref="Instance" /> is called, but not
        ///     next times.
        /// </summary>
        private Database()
        {
            _connection = new MongoClient("mongodb://127.0.0.1/pokesims").GetDatabase(DatabaseName);
        }

        /// <summary>
        ///     Single instance of the database.
        /// </summary>
        public static Database Instance => _instance ??= new Database();

        /// <summary>
        ///     Retrieve all pokemons form the database.
        /// </summary>
        /// <returns>Pokemons with all properties sync with the database, except <see cref="Pokemon.SelectedMoves" />.</returns>
        public List<Pokemon> FindAllPokemons()
        {
            IMongoCollection<BsonDocument> collection =
                _connection.GetCollection<BsonDocument>(Meta.Collection.Pokemons);
            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();
            List<Pokemon> pokemons = documents.Select(PokemonConverter.From).ToList();

            return pokemons;
        }

        /// <summary>
        ///     Retrieve a pokemon by pokedex number.
        /// </summary>
        /// <param name="dex">The unique pokedex number of the pokemon.</param>
        /// <returns>A pokemon with all properties sync with the database, except <see cref="Pokemon.SelectedMoves" /></returns>
        public Pokemon FindPokemonBy(int dex)
        {
            IMongoCollection<BsonDocument> collection =
                _connection.GetCollection<BsonDocument>(Meta.Collection.Pokemons);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("dex", dex);
            BsonDocument document = collection.Find(filter).Limit(1).First();
            Pokemon pokemon = PokemonConverter.From(document);

            return pokemon;
        }

        /// <summary>
        ///     Retrieve moves with a filter.
        /// </summary>
        /// <param name="filter">Filter using in the query.</param>
        /// <returns>Moves filtered with all properties sync with the database.</returns>
        public List<Move> FindMoves(FilterDefinition<BsonDocument> filter)
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Moves);
            List<BsonDocument> documents = collection.Find(filter).ToList();
            List<Move> moves = documents.Select(MoveConverter.From).ToList();

            return moves;
        }

        /// <summary>
        ///     Retrieve a move by name.
        /// </summary>
        /// <param name="name">The unique name of the move.</param>
        /// <returns>A move with all properties sync with the database.</returns>
        public Move FindMoveBy(string name)
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Moves);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("name", name);
            BsonDocument document = collection.Find(filter).Limit(1).First();
            Move move = MoveConverter.From(document);

            return move;
        }

        public List<Pokemon> FindAllPresets()
        {
            IMongoCollection<BsonDocument>
                collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Presets);
            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();
            List<Pokemon> presets = documents.Select(PresetConverter.From).ToList();

            return presets;
        }

        public Pokemon FindPresetBy(string objectId)
        {
            IMongoCollection<BsonDocument>
                collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Presets);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(objectId));
            BsonDocument document = collection.Find(filter).Limit(1).First();
            Pokemon preset = PresetConverter.From(document);

            return preset;
        }

        public void SavePreset(Pokemon pokemonWithAvailableMoves)
        {
            IMongoCollection<BsonDocument>
                collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Presets);
            BsonDocument document = PresetConverter.From(pokemonWithAvailableMoves);
            collection.InsertOne(document);
            pokemonWithAvailableMoves.Id = document.GetValue("_id").ToString();
        }

        public void DeletePresetBy(string presetId)
        {
            IMongoCollection<BsonDocument>
                collection = _connection.GetCollection<BsonDocument>(Meta.Collection.Presets);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(presetId));
            collection.DeleteOne(filter);
        }

        private static class Meta
        {
            public static class Collection
            {
                public const string Pokemons = "pokemons";
                public const string Moves = "moves";
                public const string Presets = "presets";
            }
        }
    }
}