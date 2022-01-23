import _ from 'lodash';
import { Pokemon } from 'pokenode-ts';
import { PokemonJSON } from '../../model';
import { AbstractTPokemonConverter } from './AbstractTPokemonConverter';

/**
 * This class is designed to use conversion methods with the class {@link PokemonJSON}.
 */
export class PokemonConverter extends AbstractTPokemonConverter {

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

  public toJSONString(pokemons: PokemonJSON[], pretty: boolean = false): string {
    const pokemonsSorted = _.sortBy(pokemons, 'dex');
    return JSON.stringify(pokemonsSorted, null, pretty ? 2 : 0);
  }
}
