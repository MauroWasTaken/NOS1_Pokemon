import { StatsJSON } from './StatJSON';
import { TypeJSON } from './TypeJSON';

export interface PokemonJSON {
  dex: number;
  name: string;
  moves: string[];
  types: TypeJSON[];
  baseStats: StatsJSON;
}
