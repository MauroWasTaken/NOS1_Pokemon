import { Fetcher } from './app/api';
import { Application, Log } from './app/app';
import { MongoDatabase } from './app/mongodb';

(async () => {
  const app = new Application();

  await Fetcher.setUp();
  Promise.all([ app.processPokemons(), app.processMoves() ]).then(() => {
    new Promise<void>(async resolve => {
      await MongoDatabase.import();
      resolve();
    }).then(() => {
      Log.info('Process terminated successfully.');
      process.exit(0);
    }).catch(reason => {
      console.error(reason);
      process.exit(1);
    });
  });
})();
