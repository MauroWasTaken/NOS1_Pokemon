import { DamageClassJSON } from './DamageClassJSON';
import { TypeJSON } from './TypeJSON';

export interface MoveJSON {
  name: string,
  power: number,
  pp: number,
  type: TypeJSON,
  accuracy: number,
  damageClass: DamageClassJSON,
  effectChance: number,
  recoilAmount: number,
  priority: number
}
