# Fullstack technical test

This project is a fullstack technical test for a job application. The project is a simple weather app that displays the weather for a given location and date.
The app uses the Open Meteo API to get the weather data.

## Technical stack

* Angular 15
* .NET 8
* PostgreSQL 14

## Documentation

### Weather API

https://open-meteo.com/en/docs/historical-weather-api

### Database

* Create a folder for DB Data: `mkdir -p c:\docker\pgdev`
* Get the Docker image: `docker pull postgres:14`
* Run the container, command below:
    ```bash
    docker run --name pgdev -e POSTGRES_PASSWORD=Str0ngP@ssword -d -p 5432:5432 -v C:\Docker\pgdev:/var/lib/postgresql/data postgres:14
    ```
