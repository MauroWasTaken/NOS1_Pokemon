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
db.createCollection('presets')
exit
```

### Insert data

Install dependencies and run the script to import JSON files to collections.

Go to the FetchPokeAPI folder and run the following commands

```shell
cd FetchPokeAPI
npm install
npm run import-data
```

## Game

Install [Unity v.2020.3.24f1](https://unity3d.com/get-unity/download/archive)

Open the project from the folder UnitySimulator.

Click on the play button.

## Documentation

A folder Doc/Data Structure is available for the structure of the MongoDb Database. There is a PDf file and a HTML folder. The HTML has a better readbility, so open the file index.html to read it.

We use a [Trello Board](https://trello.com/b/csTjQf9o) for the project management.

## Release

You can download last release in the Github release section.
