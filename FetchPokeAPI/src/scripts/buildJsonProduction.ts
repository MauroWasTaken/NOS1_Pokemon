import fs from 'fs';
import { Log } from '../app/app';

const fileFolder = './src/data';

[ 'moves', 'pokemons' ].forEach(filename => {
  fs.copyFile(`${fileFolder}/${filename}.dev.json`, `${fileFolder}/${filename}.json`, callback);
  fs.copyFile(`${fileFolder}/${filename}.min.dev.json`, `${fileFolder}/${filename}.min.json`, callback);

  function callback(err: NodeJS.ErrnoException | null) {
    if (err) {
      throw err;
    }

    Log.info('File copied successfully');
  }
});
