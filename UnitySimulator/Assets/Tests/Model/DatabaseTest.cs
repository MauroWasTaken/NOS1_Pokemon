using System.Collections.Generic;
using System.Linq;
using Model;
using NUnit.Framework;

namespace Tests.Model
{
    public class DatabaseTest
    {
        private Database _database;
        
        [SetUp]
        public void SetUp() => _database = Database.Instance;

        [Test]
        public void FindAllPokemons_BasicCase_Pokemons()
        {
            // Given
            _database = Database.Instance;

            // When
            List<Pokemon> pokemons = _database.FindAllPokemons();

            // Then
            Assert.That(pokemons.Count, Is.EqualTo(151));

            Pokemon nidoking = pokemons.Find(pokemon => pokemon.Dex == 34);
            Assert.That(nidoking.Name, Is.EqualTo("nidoking"));
            Assert.That(nidoking.Types.First(p => p.Name == "poison"), Is.Not.Null);
        }

        [Test]
        public void FindPokemonBy_Dex_Pokemon()
        {
            // Given
            _database = Database.Instance;

            // When
            Pokemon pokemon = _database.FindPokemonBy(150);

            // Then
            Assert.That(pokemon.Name, Is.EqualTo("mewtwo"));
        }
        
        [Test]
        public void FindMoveBy_Name_Move()
        {
            // Given
            _database = Database.Instance;

            // When
            Move move = _database.FindMoveBy("hyper-beam");

            // Then
            Assert.That(move.Type.Name, Is.EqualTo("normal"));
            Assert.That(move.Power, Is.EqualTo(150));
        }

        [TearDown]
        public void TearDown() => _database = null;
    }
}