using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using NUnit.Framework;

namespace Editor.Tests
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
            const int pokemonCount = 151;
            const int pokemonDex = 34;
            const string pokemonName = "nidoking";
            const string pokemonFirstType = "ground";
            const string pokemonSecondType = "poison";

            // When
            List<Pokemon> pokemons = _database.FindAllPokemons();

            // Then
            Assert.That(pokemons.Count, Is.EqualTo(pokemonCount));

            Pokemon pokemon = pokemons.Find(pokemon => pokemon.Dex == pokemonDex);
            Assert.That(pokemon.Name, Is.EqualTo(pokemonName));
            Assert.That(pokemon.Types.First(t => t.Name == pokemonFirstType), Is.Not.Null);
            Assert.That(pokemon.Types.First(t => t.Name == pokemonSecondType), Is.Not.Null);
        }

        [Test]
        public void FindPokemonBy_Dex_Pokemon()
        {
            // Given
            const int pokemonDex = 150;
            const string pokemonName = "mewtwo";
            const string pokemonType = "psychic";

            // When
            Pokemon pokemon = _database.FindPokemonBy(pokemonDex);

            // Then
            Assert.That(pokemon.Name, Is.EqualTo(pokemonName));
            Assert.That(pokemon.Types.First(t => t.Name == pokemonType), Is.Not.Null);
        }

        [Test]
        public void FindMoveBy_Name_Move()
        {
            // Given
            const string moveName = "hyper-beam";
            const string moveType = "normal";
            const int movePower = 150;

            // When
            Move move = _database.FindMoveBy(moveName);

            // Then
            Assert.That(move.Type.Name, Is.EqualTo(moveType));
            Assert.That(move.Power, Is.EqualTo(movePower));
        }

        [Test]
        public void FindAllPreset_BasicCase_Presets()
        {
            // Given

            // When

            // Then
        }

        [Test]
        public void FindPresetBy_ObjectId_Preset()
        {
            // Given
            const string presetObjectId = "61ecf0b8fa6aab7b98ab9d28";
            const int selectedMovesCount = 4;
            const string pokemonName = "alakazam";
            const string pokemonType = "psychic";
            const string moveName = "shadow-ball";
            const int spAttack = 135;

            // When
            Pokemon preset = _database.FindPresetBy(presetObjectId);

            // Then
            Assert.That(preset, Is.Not.Null);
            Assert.That(preset.SelectedMoves.Count, Is.EqualTo(selectedMovesCount));
            Assert.That(preset.Id, Is.EqualTo(presetObjectId));
            Assert.That(preset.Name, Is.EqualTo(pokemonName));
            Assert.That(preset.Types.First(t => t.Name == pokemonType), Is.Not.Null);
            Assert.That(preset.SelectedMoves.Find(m => m.Name == moveName), Is.Not.Null);
            Assert.That(preset.BaseStats.SpAttack, Is.EqualTo(spAttack));
        }

        [Test]
        public void SavePreset_BasicCase_CanDeletePreset()
        {
            // Given
            Pokemon preset = _database.FindPokemonBy(150);
            preset.SelectedMoves = preset.AvailableMoves.Take(4).ToList();

            // When
            _database.SavePreset(preset);

            // Then
            _database.DeletePreset(preset.Id);
        }

        [TearDown]
        public void TearDown() => _database = null;
    }
}