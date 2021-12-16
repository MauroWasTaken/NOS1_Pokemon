import _ from 'lodash';
import { Pokemon, PokemonStat, Type } from 'pokenode-ts';
import { PokemonJSON } from '../model/PokemonJSON';
import { StatsJSON } from '../model/StatJSON';
import { TypeJSON } from '../model/TypeJSON';
import { Fetcher } from './Fetcher';
import { Log } from './Log';

/**
 * This class is designed to convert an object to a {@link PokemonJSON} or the opposite.
 */
export class PokemonConverter {

  /**
   * Convert a pokemon object of the PokeAPI to a pokemon object of the application.
   *
   * @param pokemonFromApi
   *
   * @return The converted pokemon.
   */
  public async convertToPokemonJSON(pokemonFromApi: Pokemon): Promise<PokemonJSON> {
    return {
      dex: pokemonFromApi.id,
      name: pokemonFromApi.name,
      moves: PokemonConverter.getMovesNames(pokemonFromApi),
      baseStats: PokemonConverter.getStats(pokemonFromApi),
      types: await PokemonConverter.getTypes(pokemonFromApi)
    };
  }

  private static getMovesNames(pokemonFromApi: Pokemon): string[] {
    return pokemonFromApi.moves.map(pokemonMove => pokemonMove.move.name);
  }

  private static getStats(pokemonFromApi: Pokemon): StatsJSON {
    function getPokemonStat(statName: string): PokemonStat {
      return _.chain(pokemonFromApi.stats).filter(value => value.stat.name === statName).first().value();
    }

    return {
      hp: getPokemonStat('hp').base_stat,
      attack: getPokemonStat('attack').base_stat,
      defense: getPokemonStat('defense').base_stat,
      spAttack: getPokemonStat('special-attack').base_stat,
      spDefense: getPokemonStat('special-defense').base_stat,
      speed: getPokemonStat('speed').base_stat
    };
  }

  private static getTypes(pokemonFromApi: Pokemon): Promise<TypeJSON[]> {
    const promises = pokemonFromApi.types.map(pokemonType => {
      return Fetcher.axiosInstance.then(axiosInstance => {
        return axiosInstance.get(pokemonType.type.url).then(response => {
          Log.info('Getting type :', pokemonType.type.name);
          return typeApiToTypeApp(response.data);
        });
      });
    });

    const typeApiToTypeApp = (type: Type) => ({
      name: type.name,
      noDamageTo: type.damage_relations.no_damage_to.map(value => value.name),
      noDamageFrom: type.damage_relations.no_damage_from.map(value => value.name),
      supperEffectiveTo: type.damage_relations.double_damage_to.map(value => value.name),
      notVeryEffectiveTo: type.damage_relations.half_damage_to.map(value => value.name),
      strongAgainst: type.damage_relations.half_damage_from.map(value => value.name),
      weakAgainst: type.damage_relations.double_damage_from.map(value => value.name)
    } as TypeJSON);

    return Promise.all(promises).then(result => result);
  }

  /**
   * Convert pokemons to a JSON string.
   *
   * @param pokemons
   * @param pretty - Specifies if the JSON is in one line or readable.
   */
  public toJSONString(pokemons: PokemonJSON[], pretty: boolean = false): string {
    const pokemonsSorted = pokemons.sort((a, b) => a.dex - b.dex);
    return JSON.stringify(pokemonsSorted, null, pretty ? 2 : 0);
  }

  /**
   * Clean values to use it in MongoDB.
   * Remove '-' in name. Because Mongodb cannot have unique index with it.
   *
   * @param pokemons
   */
  public cleanValues(pokemons: PokemonJSON[]) {
    pokemons.map(pokemon => {
      pokemon.name = pokemon.name.replace('-', '');
      return pokemon;
    });
  }
}
