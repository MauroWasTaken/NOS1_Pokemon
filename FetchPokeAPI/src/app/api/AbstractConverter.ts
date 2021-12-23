import { Move, Pokemon, Type } from 'pokenode-ts';
import { Convertible } from '../../model/Convertible';
import { TypeJSON } from '../../model/TypeJSON';

export abstract class AbstractConverter {

  /**
   * Convert an object of the PokeAPI to an object of the application.
   *
   * @param objectFromApi The object from the API.
   *
   * @return The converted object within a promise.
   */
  abstract toJSONObject(objectFromApi: Pokemon | Move): Promise<Convertible>;

  /**
   * Convert an array of object of the application to a JSON string.
   *
   * @param objects The array of object to convert.
   * @param pretty - Specifies if the JSON is in one line or readable.
   *
   * @return The JSON string
   */
  abstract toJSONString(objects: Convertible[], pretty?: boolean): string;

  protected typeApiToTypeJSON(typeApi: Type): TypeJSON {
    return {
      name: typeApi.name,
      noDamageTo: typeApi.damage_relations.no_damage_to.map(value => value.name),
      noDamageFrom: typeApi.damage_relations.no_damage_from.map(value => value.name),
      supperEffectiveTo: typeApi.damage_relations.double_damage_to.map(value => value.name),
      notVeryEffectiveTo: typeApi.damage_relations.half_damage_to.map(value => value.name),
      strongAgainst: typeApi.damage_relations.half_damage_from.map(value => value.name),
      weakAgainst: typeApi.damage_relations.double_damage_from.map(value => value.name)
    };
  }
}
