import { TypeJSON } from './TypeJSON';

export interface MoveJSON {
  id: number,
  name: string,
  power: number | null,
  pp: number | null,
  type: TypeJSON,
  accuracy: number | null,
  damageClass: string | null,
  ailment: string | null,
  ailmentChance: number | null,
  recoilAmount: number | null,
  priority: -8 | -7 | -6 | -5 | -4 | -3 | -2 | -1 | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8
}
