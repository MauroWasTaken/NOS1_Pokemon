# NOS1_Pokemon

## Database

### Create MongoDB database

We use a MongoDb database with Docker.

First install Docker : <https://docs.docker.com/get-docker>.

Run a Mongo instance and launch the interactive mongo shell.

```shell
docker run --name mongodb-server -d -p 27017:27017 mongo
docker exec -it mongodb-server bash
mongosh
```

Create the database with collections.

```shell
use pokesim
db.createCollection('pokemons')
db.createCollection('moves')
exit
```

### Insert data

Install dependencies and run the script to import JSON files to collections.

```shell
npm install
npm run import-data
```

## References

