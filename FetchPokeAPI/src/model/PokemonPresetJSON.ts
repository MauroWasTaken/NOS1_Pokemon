import { Types } from 'mongoose';
import { MoveJSON, PokemonJSON } from '.';

export interface PokemonPresetJSON extends PokemonJSON {
  _id: Types.ObjectId;
  moves: MoveJSON[];
}
