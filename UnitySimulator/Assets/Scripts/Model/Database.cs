using System.Collections.Generic;
using System.Linq;
using Model.Conversion;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Model
{
    public class Database
    {
        private static Database _instance;
        private readonly IMongoDatabase _connection;

        public static Database Instance => _instance ??= new Database();

        private Database()
        {
            _connection = new MongoClient("mongodb://localhost:27017").GetDatabase("pokesim");
        }

        /// <summary>
        /// Retrieve all pokemons form the database.
        /// </summary>
        /// <returns>Pokemons with all properties sync with the database, except <see cref="Pokemon.SelectedMoves"/>.</returns>
        public List<Pokemon> FindAllPokemons()
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>("pokemons");
            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();
            List<Pokemon> pokemons = documents.Select(PokemonConverter.From).ToList();

            return pokemons;
        }

        /// <summary>
        /// Retrieve a pokemon by pokedex number.
        /// </summary>
        /// <param name="dex">The unique pokedex number of the pokemon.</param>
        /// <returns>A pokemon with all properties sync with the database, except <see cref="Pokemon.SelectedMoves"/></returns>
        public Pokemon FindPokemonBy(int dex)
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>("pokemons");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("dex", dex);
            BsonDocument document = collection.Find(filter).Limit(1).First();
            Pokemon pokemon = PokemonConverter.From(document);

            return pokemon;
        }

        /// <summary>
        /// Retrieve moves with a filter.
        /// </summary>
        /// <param name="filter">Filter using in the query.</param>
        /// <returns>Moves filtered with all properties sync with the database.</returns>
        public List<Move> FindMoves(FilterDefinition<BsonDocument> filter)
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>("moves");
            List<BsonDocument> documents = collection.Find(filter).ToList();
            List<Move> moves = documents.Select(MoveConverter.From).ToList();

            return moves;
        }

        /// <summary>
        /// Retrieve a move by name.
        /// </summary>
        /// <param name="name">The unique name of the move.</param>
        /// <returns>A move with all properties sync with the database.</returns>
        public Move FindMoveBy(string name)
        {
            IMongoCollection<BsonDocument> collection = _connection.GetCollection<BsonDocument>("moves");
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("name", name);
            BsonDocument document = collection.Find(filter).Limit(1).First();
            Move move = MoveConverter.From(document);

            return move;
        }
    }
}