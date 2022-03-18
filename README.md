# Store API

A demo API for stock management.

# Sections
- [About](#about)
- [Testing this API](#testing-this-api)
	- [Online demo](#online-demo)
	- [Using Docker containers](#run-locally-with-docker-containers)
		- [Prerequisites](#prerequisites)
		- [With ready-to-use shell scripts](#running-premade-shell-scripts)
		- [With docker compose](#with-docker-compose)
        - [From the source code](#running-from-source-code)

# About
## O que é 
Essa é uma API REST demonstrativa para gerenciamento de estoque, com autenticação JWT e autorização role-based. 

## Tecnologias usadas
- ASP.NET Core 3.1
- Entity Framework Core 
> Foi utilizado a abordagem code-first 
- SQLServer 2019
- Identity
- Docker
- Swagger
> Swashbuckle
- FluentValidation
- AutoMapper
- <details><summary>CLIQUE</summary>
    - aa  
    - bb
    - cc
    - dd
    </details>
- MaisUmaTecnologia
- MaisUmaOutraTecnologia

## Architecture remarks
- Um dos propósitos desse projeto é demonstrar a implementação de alguns padrões de projeto. Alguns deles são:
    - Usa uma arquitetura em 3-layers (*DAL, Services and the API*), em um único projeto.
    - Repository pattern
    - Unit of Work pattern (sobre o EF Core)
    - Problem Details como resposta de erros padrão
    - SOLID (*espalhado pelo código*)    
    - Request-Response for services communication

- Middleware para tratamento de exceções
- FluentValidation does not work with primitive types, so it was necessary to create a custom validation (AppCustomValidation). The design choice was to keep it fluent with method chaining with similar naming to FluentValidation.
- The classes `ServiceResponse` and `ServiceResponse<T>` are the default responses from the service layer. They are the base classes for `FailedServiceResponse` and `FailedServiceResponse<T>` respectively, and offer methods in a fluent way to configure the appropriate ProblemDetails response.



 

## Clone this project

On the terminal, enter: 
```
git clone https://github.com/ThiagoBerrutti/sales-api.git
```

  

# Testing this API  

## Online demo

To test the api online, go to https://store-api-demo.herokuapp.com/swagger/index.html

  

## Run locally with Docker containers
### Prerequisites

This project was fully developed and runs online using Docker containers. To test it locally, you need to have [Docker installed](https://docs.docker.com/get-docker/).

If any errors occur during the containers creation while following the instructions below, make sure that the ports **1401** and **5000** are not being used by another container.
> Note: by default, the **SQLServer** container runs on port *1401* and the **API** container on port *5000*


### Running ready-to-use shell scripts

To make easier to run the project, there are some premade shell scripts that pull and runs the containers

1. from the root folder, enter the folder './setup'
2. run the files:
	- `1-docker-network.sh`
	- `2-docker-sqlserver.sh`
	- `3-docker-store-api.sh`
3. wait for the containers to finish loading
4. visit http://localhost:5000/swagger/index.html, should work  

### With docker compose
1. on the terminal, go to project root folder, where the file **`docker-compose.yaml`** is
3. enter the command :
`docker compose up`
4. wait for the containers to finish loading
5. visit http://localhost:5000/swagger/index.html, should work

### Running from source code

1. run the SQLServer container using one of the following methods:
	- with premade shell scripts:
		-  from the root folder, enter folder `'./setup'`
		- execute the file **`2-docker-sqlserver.sh`**
	-  with terminal command line:
	- <details>
		- 	On the terminal, enter the command: 
```sh
docker run -d -p 1401:1433 --name sqlserver -e ACCEPT_EULA=Y -e SA_PASSWORD=1q2w3e4r@#$ mcr.microsoft.com/mssql/server:latest
```
</details>


2. run the cloned project (i.e: on Visual Studio)  

Enter http://localhost:5000/swagger, should work