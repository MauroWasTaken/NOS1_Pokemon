import { Types } from 'mongoose';
import { BaseURL, Endpoints, Pokemon } from 'pokenode-ts';
import { MoveJSON } from '../../model';
import { PokemonPresetJSON } from '../../model/PokemonPresetJSON';
import { AbstractTPokemonConverter } from './AbstractTPokemonConverter';
import { Fetcher } from './Fetcher';
import { MoveConverter } from './MoveConverter';

export class PresetConverter extends AbstractTPokemonConverter {

  public async toJSONObject(pokemonFromApi: Pokemon): Promise<PokemonPresetJSON> {
    let preset: PokemonPresetJSON;

    switch (pokemonFromApi.id) {
      case 151:
      default:
        preset = await this.createMew(pokemonFromApi);
        break;
      case 65:
        preset = await this.createAlakazam(pokemonFromApi);
        break;
      case 112:
        preset = await this.createRhydon(pokemonFromApi);
        break;
    }

    return preset;
  }

  toJSONString(presets: PokemonPresetJSON[], pretty?: boolean): string {
    return JSON.stringify(presets, null, pretty ? 2 : 0);
  }

  private async createMew(pokemonFromApi: Pokemon): Promise<PokemonPresetJSON> {
    const objectId = new Types.ObjectId('61ecf0b8fa6aab7b98ab9d27');
    const movesName = [ 'psychic', 'thunderbolt', 'flamethrower', 'ice-beam' ];

    return await this.getPreset(pokemonFromApi, objectId, movesName);
  }

  private async createAlakazam(pokemonFromApi: Pokemon): Promise<PokemonPresetJSON> {
    const objectId = new Types.ObjectId('61ecf0b8fa6aab7b98ab9d28');
    const movesName = [ 'psychic', 'focus-blast', 'energy-ball', 'shadow-ball' ];

    return await this.getPreset(pokemonFromApi, objectId, movesName);
  }

  private async createRhydon(pokemonFromApi: Pokemon): Promise<PokemonPresetJSON> {
    const objectId = new Types.ObjectId('61ecf0b8fa6aab7b98ab9d29');
    const movesName = [ 'stone-edge', 'earthquake', 'iron-tail', 'brick-break' ];

    return await this.getPreset(pokemonFromApi, objectId, movesName);
  }

  private async getPreset(pokemonFromApi: Pokemon, objectId: Types.ObjectId, movesName: string[]) {
    return {
      _id: objectId,
      dex: pokemonFromApi.id,
      name: pokemonFromApi.name,
      types: await this.getTypes(pokemonFromApi),
      baseStats: PresetConverter.getStats(pokemonFromApi),
      moves: await this.getMoves(movesName)
    } as PokemonPresetJSON;
  }

  private async getMoves(names: string[]): Promise<Awaited<MoveJSON>[]> {
    const promise: Promise<MoveJSON>[] = names.map(async name => {
      const endpoint = `${BaseURL.REST}${Endpoints.Move}/${name}`;
      return Fetcher.axiosInstance.then(axiosInstance => {
        return axiosInstance.get(endpoint).then(async response => {
          return await new MoveConverter().toJSONObject(response.data);
        });
      });
    });

    return Promise.all(promise).then(result => result);
  }
}
