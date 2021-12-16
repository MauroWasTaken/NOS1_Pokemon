import { Fetcher } from './app/Fetcher';
import { createPokemonJsonFile } from './scripts/script';

(async () => {
  await Fetcher.setUp();
  createPokemonJsonFile().catch(reason => console.error(reason));
})();

