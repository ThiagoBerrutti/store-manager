# Store API
A demo API for stock management. 

# Sections
- [Testing this API](#testing-this-api)
    - [Online demo](#online-demo)
    - [Using Docker containers](#run-locally-with-docker-containers)
        - [Prerequisites](#prerequisites)
        - [Running ready-to-use shell scripts](#running-premade-shell-scripts)
        - []

## Clone this project
To clone the git repository, enter on terminal: 
`git clone https://github.com/ThiagoBerrutti/sales-api.git`

# Testing this API

## Online demo
To test the api online, go to https://store-api-demo.herokuapp.com/swagger/index.html

## Run locally with Docker containers

### Prerequisites
This project was fully developed and runs online using Docker containers. To test it locally, you need to have [Docker installed](https://docs.docker.com/get-docker/).

If any errors occur during the containers creation while following the instructions below, make sure that the ports **1401** and **5000** are not being used by another container. 

(_By default, the SQLServer container runs on port 1401 and the API container on port 5000_)

### Running ready-to-use shell scripts
To make easier to run the project, there are some premade shell scripts that pull and runs the containers
1. from the root folder, enter the folder './setup'
2. run:
- 1-docker-network
- 2-docker-sqlserver
- 3-docker-store-api
3. wait for the containers to finish loading

Enter http://localhost:5000/swagger, should work

### With docker compose 
1. on terminal, go to project root folder, where the file **'docker-compose.yaml'** is
2. enter the command `docker compose up`,
3. wait for the containers to finish loading

Enter http://localhost:5000/swagger, should work

### Running from source code
1. from the root folder, enter folder './setup'
2. execute '2-docker-sqlserver'
        OR
   on terminal, enter the command 
```sh
docker run -d \
    -p 1401:1433 \
    --name sqlserver \
    -e ACCEPT_EULA=Y \
    -e SA_PASSWORD=1q2w3e4r@#$ \
    mcr.microsoft.com/mssql/server:latest
```
3. run the cloned project

Enter http://localhost:5000/swagger, should work



