# FetchPokeAPI

FetchPokeAPI is a nodejs application written in typescript designed to fetch the PokeAPIv2 and convert it into JSON
files that can be used in a MongoDB database.

The JSON outputs are light versions of the ones from the API, because we don't need all the data of the API.

## Requirements

To run the project you only need npm :

| Tools                                         | Version used in this project  |
|-----------------------------------------------|-------------------------------|
| [Npm](https://nodejs.org/en/download/)        | 8.1.4                         |

## Installation

```bash
git clone https://github.com/maurxsantoz/NOS1_Pokemon.git
cd FetchPokeAPI
npm install
```

## Usage

There are two ways to run the script that fetch the API :

1. Run directly the TypeScript files :
    ```bash
    npm run start:dev
    ```
2. To run javascript build files :
    ```bash
    npm run build
    npm start
    ```

These commands will create 2 JSON files in the folder `src/data` :

- `pokemon.dev.json`
- `pokemon.min.dev.json`

Once you finish the development, build JSON production files with the npm script :

```bash
npm run build:json-file
```

It uses the file `buildJsonProduction.js` to copy the `.dev.json` files to `.json` files - .dev.json files are ignored
from git.
