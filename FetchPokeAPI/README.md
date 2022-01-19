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

```shell
git clone https://github.com/maurxsantoz/NOS1_Pokemon.git
cd FetchPokeAPI
npm install
```

### Docker

Next installations use Docker.

Go to <https://docs.docker.com/get-docker/> and follow instructions.

### Redis

The application use a redis cache to store data and avoid always get them from the web.

```shell
docker run -d --name redis-server -p 127.0.0.1:6379:6379 redis
```

### MongoDB

If you choose to sync the database after the files are created

```shell
docker run --name mongodb-server -d -p 27017:27017 mongo
docker exec -it mongodb-server bash
mongo
```

You will be in a mongo interactive shell, run the commands below to create the database with collections.

```shell
use pokesim
db.createCollection('pokemons')
db.createCollection('moves')
exit
```

## Usage

```shell
npm run start:dev
```

_The first time, depends on your quality connection, the process can take some time, or you will need to rerun the
command if it freezes. It's because sometimes, the internet connection can be lost, independently of the program. The
advantage of restart is that all successful request is now stored in the redis cache._

These commands will create 2 JSON files in the folder `src/data` :

- `pokemon.dev.json`
- `pokemon.min.dev.json`

Once you finish the development, build JSON production files with the npm script :

```shell
npm run copy-json-files
```

It uses the file `buildJsonProduction.ts` to copy the `.dev.json` files to `.json` files - .dev.json files are ignored
from git.

Finally, sync the prod files with the database :

```shell
npm run import-data
```

You can also make all the steps with one command :

```shell
npm run start:sync
```
