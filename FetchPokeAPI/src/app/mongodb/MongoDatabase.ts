import mongoose, { Schema } from 'mongoose';
import { MoveJSON, PokemonJSON } from '../../model';
import { Log } from '../app';
import { IO } from '../io';

/**
 * This class is designed to interact with the Mongo database.
 */
export class MongoDatabase {

  //region Constants

  private static readonly collectionsName: [ 'pokemons', 'moves' ] = [ 'pokemons', 'moves' ];

  //endregion

  //region Public methods

  /**
   * Import data from JSON files into the Mongo Database.
   */
  public static async import(): Promise<void> {
    await MongoDatabase.connect().then(async () => {
      await MongoDatabase.truncateCollections();
      await MongoDatabase.insertData();
      mongoose.connection.close().then(() => {
        Log.info('Mongo connection closed');
      });
    }).catch(err => Log.error(err));
  }

  //endregion

  //region Private methods

  private static async connect(): Promise<void> {
    const uri = 'mongodb://localhost:27017/pokesim';

    await mongoose.connect(uri).then(() => Log.info('Connected to Mongo database'));
  }

  private static async truncateCollections() {
    for (const collectionName of this.collectionsName) {
      await mongoose.connection.db.collection(collectionName).deleteMany({});
      Log.info('Truncated data of collection : ', collectionName);
    }
  }

  private static async insertData() {
    for (const collectionName of this.collectionsName) {
      const schema = this.getSchema(collectionName);
      const Model = mongoose.model(collectionName, schema);
      const data = IO.readAsJSONObject(`src/data/${collectionName}.min.json`);

      await Model.insertMany(data).then(() => Log.info('Imported JSON file to collection : ', collectionName));
    }
  }

  private static getSchema(schemaName: 'pokemons' | 'moves'): Schema {
    switch (schemaName) {
      case 'pokemons':
        return new Schema<PokemonJSON>({
          dex: { type: Number, required: true, unique: true },
          name: { type: String, required: true, unique: true, index: 'text' },
          moves: { type: [ String ], required: true },
          types: { type: [ Object ], required: true },
          baseStats: { type: Object, required: true }
        });
      case 'moves':
        return new Schema<MoveJSON>({
          id: { type: Number, required: true, unique: true },
          name: { type: String, unique: true, index: 'text' },
          power: { type: Number },
          pp: { type: Number },
          type: { type: Object },
          accuracy: { type: Number },
          damageClass: { type: String },
          ailment: { type: String },
          ailmentChance: { type: Number },
          recoilAmount: { type: Number }
        });
    }
  }

  //endregion
}
