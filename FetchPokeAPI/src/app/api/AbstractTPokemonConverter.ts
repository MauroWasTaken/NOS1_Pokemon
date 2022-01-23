import _ from 'lodash';
import { Pokemon, PokemonStat } from 'pokenode-ts';
import { StatsJSON, TypeJSON } from '../../model';
import { AbstractConverter } from './AbstractConverter';
import { Fetcher } from './Fetcher';

export abstract class AbstractTPokemonConverter extends AbstractConverter {

  protected static getStats(pokemonFromApi: Pokemon): StatsJSON {
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

  protected getTypes(pokemonFromApi: Pokemon): Promise<TypeJSON[]> {
    const promises = pokemonFromApi.types.map(pokemonType => {
      return Fetcher.axiosInstance.then(axiosInstance => {
        return axiosInstance.get(pokemonType.type.url).then(response => {
          return this.typeApiToTypeJSON(response.data);
        });
      });
    });

    return Promise.all(promises).then(result => result);
  }
}
