import _ from 'lodash';
import { BaseURL, Endpoints } from 'pokenode-ts';
import { Convertible, MoveJSON, PokemonJSON } from '../../model';
import { AbstractConverter, Fetcher, MoveConverter, PokemonConverter } from '../api';
import { IO } from '../io';
import { Log } from './Log';

export class Application {

  async processPokemons(): Promise<void> {
    return new Promise<void>(resolve => {
      const firstGen: number[] = _.range(1, 151 + 1);
      const pokemons: PokemonJSON[] = [];
      const pokemonConverter = new PokemonConverter();
      const endPoint = `${BaseURL.REST}${Endpoints.Pokemon}`;

      Promise.all(firstGen.map(dexNumber => this.fetch(dexNumber, endPoint, pokemons, pokemonConverter))).then(() => {
        pokemonConverter.cleanValues(pokemons);
        IO.write('src/data/pokemons.min.dev.json', pokemonConverter.toJSONString(pokemons));
        IO.write('src/data/pokemons.dev.json', pokemonConverter.toJSONString(pokemons, true));
        resolve();
      }).catch(reason => Log.error(reason));
    });
  }

  async processMoves(): Promise<void> {
    return new Promise<void>(resolve => {
      const movesId: number[] = _.range(1, 826 + 1);
      const moves: MoveJSON[] = [];
      const endPoint = `${BaseURL.REST}${Endpoints.Move}`;
      const moveConverter = new MoveConverter();

      Promise.all(movesId.map(moveId => this.fetch(moveId, endPoint, moves, moveConverter))).then(() => {
        IO.write('src/data/moves.min.dev.json', moveConverter.toJSONString(moves));
        IO.write('src/data/moves.dev.json', moveConverter.toJSONString(moves, true));
        resolve();
      });
    });
  }

  private fetch(
      id: number,
      endPoint: string,
      JSONObjects: Convertible[],
      converter: AbstractConverter
  ): Promise<void> {
    return new Promise<void>(async resolve => {
      Fetcher.axiosInstance.then(axios => axios.get(`${endPoint}/${id}`).then(response => {
        converter.toJSONObject(response.data).then((JSONObject: Convertible) => {
          const endMessage = response.request.fromCache ? 'Redis cache' : 'API';
          Log.info(`Resolving ${JSONObject.name.toUpperCase()} from ${endMessage}`);
          JSONObjects.push(JSONObject);
          resolve();
        }).catch(reason => Log.error(reason));
      }).catch(reason => Log.error(reason)));
    });
  }
}
