import { Fetcher } from '../app/api';
import { Application, Log } from '../app/app';

(async () => {
  const app = new Application();

  await Fetcher.setUp();
  Promise.all([ app.processPokemons(), app.processMoves() ]).then(() => {
    Log.info('Fetch process terminated successfully.');
    process.exit(0);
  }).catch(reason => {
    console.error(reason);
    process.exit(1);
  });
})();
