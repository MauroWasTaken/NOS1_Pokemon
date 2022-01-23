import { StatsJSON } from './StatJSON';
import { TypeJSON } from './TypeJSON';

export type TPokemon = {
  dex: number;
  name: string;
  moves: unknown;
  types: TypeJSON[];
  baseStats: StatsJSON;
}
