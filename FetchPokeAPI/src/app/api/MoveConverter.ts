import _ from 'lodash';
import { Move } from 'pokenode-ts';
import { MoveJSON } from '../../model/MoveJSON';
import { AbstractConverter } from './AbstractConverter';
import { Fetcher } from './Fetcher';

/**
 * This class is designed to use conversion methods with the class {@link MoveJSON}.
 */
export class MoveConverter extends AbstractConverter {

  public async toJSONObject(moveFromApi: Move): Promise<MoveJSON> {
    return {
      id: moveFromApi.id,
      name: moveFromApi.name,
      type: await this.getTypes(moveFromApi),
      damageClass: moveFromApi.damage_class?.name ?? null,
      pp: moveFromApi.pp,
      power: moveFromApi.power,
      accuracy: moveFromApi.accuracy,
      priority: moveFromApi.priority,
      ailment: moveFromApi.meta?.ailment.name ?? null,
      ailmentChance: moveFromApi.meta?.ailment_chance ?? null,
      recoilAmount: moveFromApi.meta?.drain ?? null
    };
  }

  toJSONString(moves: MoveJSON[], pretty: boolean = false): string {
    const movesSorted = _.sortBy(moves, 'name');
    return JSON.stringify(movesSorted, null, pretty ? 2 : 0);
  }

  private async getTypes(moveFromApi: Move) {
    return Fetcher.axiosInstance.then(axiosInstance => {
      return axiosInstance.get(moveFromApi.type.url).then(response => {
        return this.typeApiToTypeJSON(response.data);
      });
    });
  }
}
