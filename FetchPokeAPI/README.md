# FetchPokeAPI

FetchPokeAPI is a nodejs application written in typescript designed to fetch the PokeAPIv2 and convert it into JSON 
files that can be used in a MongoDB database.

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

_These commands will create 2 JSON files in the folder `src/data` :_

- _`pokemon.dev.json`_
- _`pokemon.min.dev.json`_

_Rename the by deleting the '.json' if you want to update the production files._

### Development

```bash
npm run start:dev
```

### Production

```bash
npm run build
npm start
```
