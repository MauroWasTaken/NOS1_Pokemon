import _ from 'lodash';
import { BaseURL, Endpoints } from 'pokenode-ts';
import { AbstractConverter } from '../app/api/AbstractConverter';
import { Fetcher } from '../app/api/Fetcher';
import { MoveConverter } from '../app/api/MoveConverter';
import { PokemonConverter } from '../app/api/PokemonConverter';
import { FileWriter } from '../app/FileWriter';
import { Log } from '../app/Log';
import { Convertible } from '../model/Convertible';
import { MoveJSON } from '../model/MoveJSON';
import { PokemonJSON } from '../model/PokemonJSON';

export async function createPokemonJsonFile(): Promise<void> {
  return new Promise<void>(resolve => {
    const firstGen: number[] = _.range(1, 151 + 1);
    const pokemons: PokemonJSON[] = [];
    const pokemonConverter = new PokemonConverter();
    const endPoint = `${BaseURL.REST}${Endpoints.Pokemon}`;

    Promise.all(firstGen.map(dexNumber => fetch(dexNumber, endPoint, pokemons, pokemonConverter))).then(() => {
      pokemonConverter.cleanValues(pokemons);
      new FileWriter().write('src/data/pokemons.min.dev.json', pokemonConverter.toJSONString(pokemons));
      new FileWriter().write('src/data/pokemons.dev.json', pokemonConverter.toJSONString(pokemons, true));
      resolve();
    }).catch(reason => Log.error(reason));
  });
}

export async function createMoveJsonFile(): Promise<void> {
  return new Promise<void>(resolve => {
    const movesId: number[] = _.range(1, 826 + 1);
    const moves: MoveJSON[] = [];
    const endPoint = `${BaseURL.REST}${Endpoints.Move}`;
    const moveConverter = new MoveConverter();

    Promise.all(movesId.map(moveId => fetch(moveId, endPoint, moves, moveConverter))).then(() => {
      new FileWriter().write('src/data/moves.min.dev.json', moveConverter.toJSONString(moves));
      new FileWriter().write('src/data/moves.dev.json', moveConverter.toJSONString(moves, true));
      resolve();
    });
  });
}

function fetch(
    id: number,
    endPoint: string,
    JSONObjects: Convertible[],
    converter: AbstractConverter
): Promise<void> {
  return new Promise<void>(async resolve => {
    Fetcher.axiosInstance.then(axios => axios.get(`${endPoint}/${id}`).then(response => {
      converter.toJSONObject(response.data).then((JSONObject: Convertible) => {
        Log.info('Resolving :', JSONObject.name);
        JSONObjects.push(JSONObject);
        resolve();
      }).catch(reason => Log.error(reason));
    }).catch(reason => Log.error(reason)));
  });
}

module.exports = { createPokemonJsonFile, createMoveJsonFile };
