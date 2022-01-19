using System.Collections.Generic;
using MongoDB.Driver;

namespace Model
{
    public class Database
    {
        private Database()
        {
            
        }

        public static Database Instance { get; } = new Database();

        public List<Pokemon> FindAllPokemons()
        {
            throw new System.NotImplementedException();
        }
    }
}