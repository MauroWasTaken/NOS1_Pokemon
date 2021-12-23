import { Fetcher } from './app/api/Fetcher';
import { Log } from './app/Log';
import { createMoveJsonFile, createPokemonJsonFile } from './scripts/script';

(async () => {
  await Fetcher.setUp();
  await Promise.all([ createPokemonJsonFile(), createMoveJsonFile() ]).then(() => {
    Log.info('Process terminated successfully.');
    process.exit(0);
  });
})();
