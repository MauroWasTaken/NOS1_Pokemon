import _ from 'lodash';
import { Pokemon, PokemonStat } from 'pokenode-ts';
import { PokemonJSON, StatsJSON, TypeJSON } from '../../model';
import { AbstractConverter } from './AbstractConverter';
import { Fetcher } from './Fetcher';

/**
 * This class is designed to use conversion methods with the class {@link PokemonJSON}.
 */
export class PokemonConverter extends AbstractConverter {

  public async toJSONObject(pokemonFromApi: Pokemon): Promise<PokemonJSON> {
    return {
      dex: pokemonFromApi.id,
      name: pokemonFromApi.name,
      types: await this.getTypes(pokemonFromApi),
      baseStats: PokemonConverter.getStats(pokemonFromApi),
      moves: PokemonConverter.getMovesNames(pokemonFromApi)
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

  private getTypes(pokemonFromApi: Pokemon): Promise<TypeJSON[]> {
    const promises = pokemonFromApi.types.map(pokemonType => {
      return Fetcher.axiosInstance.then(axiosInstance => {
        return axiosInstance.get(pokemonType.type.url).then(response => {
          return this.typeApiToTypeJSON(response.data);
        });
      });
    });

    return Promise.all(promises).then(result => result);
  }

  public toJSONString(pokemons: PokemonJSON[], pretty: boolean = false): string {
    const pokemonsSorted = _.sortBy(pokemons, 'dex');
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
