const fs = require("fs");

const fileFolder = "./src/data";

["moves", "pokemons"].forEach(filename => {
  fs.copyFile(`${fileFolder}/${filename}.dev.json`, `${fileFolder}/${filename}.json`, callback);
  fs.copyFile(`${fileFolder}/${filename}.min.dev.json`, `${fileFolder}/${filename}.min.json`, callback);

  function callback(err) {
    if (err) {
      throw err;
    }
    console.log("Copy completed successfully");
  }
});
