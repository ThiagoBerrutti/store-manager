# Store Manager
A demonstration REST API for store management, using JWT authentication and role-based authorization. 

## About
- [Technologies used](#technologies-used)	
- [Project remarks](#project-remarks)
- [Clone this project](#clone-this-project)
- [How to run this API](#how-to-run-this-api)	
- [API](#api)
	- [What it does](#what-it-does)

> See the [Wiki page](https://github.com/ThiagoBerrutti/store-manager/wiki) for more info

## Technologies used
- ASP.NET Core 3.1
- Entity Framework Core
- SQLServer 2019
- Identity
- Docker ([see more](https://github.com/ThiagoBerrutti/store-manager/wiki/Setup-Docker-containers))
- Swagger
- FluentValidation
- AutoMapper

## Project remarks
- One of the purposes of this project is the implementation of some patterns. Some of these are:
    - 3-layer architecture (*DAL, Services and the API*), in a single project.
    - Repository pattern
    - Unit of Work pattern (on top of EF Core)
    - Problem Details as a standard error response
    - SOLID
    - Request-Response for services communication
- Uses a middleware for exception handling
- Consumes external API for random user generation
- Documented in detail on swagger 


# Clone this project
```sh
git clone https://github.com/ThiagoBerrutti/sales-api.git
```

# How to run this API  
You have two alternatives: 
- test an online<sup>1</sup> version of this API on https://store-api-demo.herokuapp.com/swagger/index.html
- use Docker containers to setup the SQLServer and the API locally. See [Setup Docker containers](https://github.com/ThiagoBerrutti/store-manager/wiki/Setup-Docker-containers) for more info
 
*[1] The online API and database are hosted separately (on Heroku and Somee, respectively), so online testing depends on both hosts availability*

# API
## What it does
Although this API provides endpoints to perform operations with products and stock control, the focus of this demo is about the authentication/authorization process it uses. 

It uses a *role-based authorization*, that is, access to the API endpoints depends on the roles assigned to the user.

Upon registration, an user has no roles assigned. To have roles assigned to it, after the user registration an *Administrator* or *Manager* should do the operation.

## See on wiki:
- [API](https://github.com/ThiagoBerrutti/store-manager/wiki/API)
    - [Entities](https://github.com/ThiagoBerrutti/store-manager/wiki/API.Entities)
    - [Operations](https://github.com/ThiagoBerrutti/store-manager/wiki/API.Operations)
    - [Responses](https://github.com/ThiagoBerrutti/store-manager/wiki/API.Responses)
    - [Quick Testing](https://github.com/ThiagoBerrutti/store-manager/wiki/API.QuickTesting)
