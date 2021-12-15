import { AxiosResponse } from 'axios';
import _ from 'lodash';
import { BaseURL, Endpoints, Pokemon } from 'pokenode-ts';
import { Fetcher } from '../app/Fetcher';
import { FileWriter } from '../app/FileWriter';
import { Log } from '../app/Log';
import { PokemonConverter } from '../app/PokemonConverter';
import { PokemonJSON } from '../model/PokemonJSON';

export async function createPokemonJsonFile() {
  const firstGen: number[] = _.range(1, 151 + 1);
  const pokemonConverter = new PokemonConverter();
  const endPoint = `${BaseURL.REST}${Endpoints.Pokemon}`;
  let pokemons: PokemonJSON[] = [];

  const promises: Promise<void>[] = firstGen.map(dexNumber => {
    return new Promise<void>(async resolve => {
      function convertToPokemonJSON(response: AxiosResponse<Pokemon>) {
        pokemonConverter.convertToPokemonJSON(response.data).then((pokemonJSON: PokemonJSON) => {
          Log.info('Resolving pokemon :', pokemonJSON.name);
          pokemons.push(pokemonJSON);
          resolve();
        }).catch(reason => Log.error(reason));
      }

      Fetcher.axiosInstance.get(`${endPoint}/${dexNumber}`).then(convertToPokemonJSON).catch(reason => Log.error(reason));
    });
  });

  Promise.all(promises).then(() => {
    pokemonConverter.cleanValues(pokemons);
    new FileWriter().write('src/data/pokemons.min.dev.json', pokemonConverter.toJSONString(pokemons));
    new FileWriter().write('src/data/pokemons.dev.json', pokemonConverter.toJSONString(pokemons, true));
    Log.info('Process terminated successfully.')
  }).catch(reason => Log.error(reason));
}

module.exports = { createPokemonJsonFile };
