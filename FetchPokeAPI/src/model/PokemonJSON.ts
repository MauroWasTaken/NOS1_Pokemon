import { MoveJSON } from '.';
import { TPokemon } from './TPokemon';

export interface PokemonJSON extends TPokemon {
  moves: string[] | MoveJSON[];
}
