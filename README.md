# Store API

A demo API for stock management.

# Sections
- [About](#about)
	- [What it is](#what-it-is)
	- [Technologies used](#technologies-used)
	- [Architecture remarks](#architecture-remarks)
	- [Other remarks](#other-remarks)
- [Clone this project](#clone-this-project)
- [How to run this API](#how-to-run-this-api)
	- [Online demo](#1-online-demo)
	- [Using Docker containers](#2-run-locally-with-docker-containers)
		- [Prerequisites](#prerequisites)
		- [Setup](#setup)
		- [With docker compose](#with-docker-compose)
		- [With ready-to-use shell scripts](#running-premade-shell-scripts)
        - [From the source code](#running-from-source-code)
- [API](#api)
	- [What it does](#what-it-does)
	- [Entities](#entities)
		- [Product](#product)
		- [Product stock](#product-stock)
		- [Role](#role)
		- [User](#user)
	- [Test users](#test-users)
	- [Operations](#operations)
		- [Authentication and Authorization](#authentication-and-authorization)
		- [Products](#products)
		- [Stock](#stock)
		- [Roles](#roles)
		- [Users](#users)
	- [Responses](#responses)
		- [Pagination](#pagination)
		- [Error responses](#error-responses)


# About
## What it is
This is a demonstration REST API for store management, using JWT authentication and role-based authorization. 

## Technologies used
- ASP.NET Core 3.1
- Entity Framework Core (*code first approach*)
- SQLServer 2019
- Identity
- Docker ([see more](#run-locally-with-docker-containers))
- Swagger
- FluentValidation
- AutoMapper

## Architecture remarks
- One of the purposes of this project is to demonstrate the implementation of some standards. Some of these are:
    - 3-layer architecture (*DAL, Services and the API*), in a single project.
    - Repository pattern
    - Unit of Work pattern (on top of EF Core)
    - Problem Details as a standard error response
    - SOLID (*over the code*)    
    - Request-Response for services communication
- Middleware for exception handling
 
## Other remarks
- Consumes external API for random user generation

# Clone this project
```
git clone https://github.com/ThiagoBerrutti/sales-api.git
```

# How to run this API  
You have two alternatives: 
1. you can [test it online](#1-online-demo) on production or..
2. you can [use Docker containers](#2-run-locally-with-docker-containers) to setup the SQLServer and the API locally.

## 1. Online demo
You can test a live version of this API on https://store-api-demo.herokuapp.com/swagger/index.html
> The API and the database are hosted separately, so online testing depends on both hosts availability


## 2. Run locally with Docker containers
### Prerequisites

This project was fully developed and runs online using Docker containers. To test it on your local machine, make sure to have [Docker installed](https://docs.docker.com/get-docker/).

>Note: for this project, the **SQLServer** container runs on port *1401* and the **API** container on port *5000*. Make sure both ports are not being used before creating the containers.
### Setup

There are three methods to setup it:
- [using `docker compose` command](#with-docker-compose) 
- [running ready-to-use premade shell scripts](#running-ready-to-use-shell-scripts)
- [run it from the source code](#running-from-source-code)


### With docker compose

1. on terminal, go to project root folder, where the file **`docker-compose.yaml`** is
3. enter the command 
```sh
 docker compose up
 ```
4. wait for the containers to finish loading
5. go to http://localhost:5000/swagger/index.html, should work

### Running ready-to-use shell scripts

To make easier to run the project, there are some premade shell scripts that pull and runs the containers
1. from the root folder, enter the folder './setup'
2. run the files:
	- `1-docker-network.sh`
	- `2-docker-sqlserver.sh`
	- `3-docker-store-api.sh`
3. wait for the containers to finish loading
4. go to http://localhost:5000/swagger/index.html, should work  


### Running from source code

1. run the SQLServer container using **one of the following methods**:
	- from premade shell scripts:
		- from the root folder, enter folder `'./setup'`
		- execute the file **`2-docker-sqlserver.sh`**						
	-  from terminal command line:
		1. On the terminal, enter the command: 
		```sh
		docker run -d -p 1401:1433 --name sqlserver -e ACCEPT_EULA=Y -e SA_PASSWORD=1q2w3e4r@#$ mcr.microsoft.com/mssql/server:latest
		```
2. run the cloned project 
3. go to http://localhost:5000/swagger, should work

# API
## What it does
Although this API provides endpoints to perform operations with products and stock control, the focus of this demo is about the authentication/authorization process it uses. 

It uses a *role-based authorization*, that is, access to endpoints depends on the roles assigned to the user.

Upon registration, an user has no roles assigned. To have roles assigned to it, after the user registration an *Administrator* or *Manager* should do the operation.


## Entities
The entities are *purposely short and have very few fields*. This can lead to features sometimes look as overkill (i.e. `Stock` being a whole separate entity only to store the `Quantity` field?) or sometimes missing (i.e. no Role update operation? The reason is that Roles only have the `Name` field, which acts as an identifier and can't be modified). As soon as the project expands, these inconsistences will no longer be.

### Product
Represents a product the store works with. Contains information about this product such as name, price and description. For simplicity, the products are not treated individually, that said, multiple of the same product have exactly the same data and are only represented by it's *Quantity* on stock.

### Product stock
Representation of a product on stock. Contains information about the product stored on stock. As products are simplified for all having the same data, it's representation on the stock is just the *Quantity* of items.

For this demo we will keep at this, but other fields could include the supplier data, purchasing data, value on stock, a specific product localization etc.

### Role
Represents the role a user have on the business. There are four roles with different levels of authorization, namely *Administrator*, *Manager*, *Stock* and *Seller*. Each role give access to a different collection of operations.

| Role | Access to | Role Id |
| --- | --- | --- |
| Administrator | All operations. Administrator role can only be assigned and removed by the root Administrator. This role can run very sensible operations, as forcefully changing/resetting passwords and delete Roles | 1 |
| Manager | Most operations, except sensible ones *(i.e. forcefully changing passwords, delete roles etc)*. Managers are able to edit user data, create and assign roles, insert/delete products from the database etc. | 2 |
| Stock | All stock-related operations, including add/remove/edit product stocks  | 3|
| Seller | Stock GET operations | 4 |
| *Other/No role* | Current user operations and GET Product operations | --- |

Other roles can be created and assigned, although custom authorization for this new roles will demand changes on the code.


### User
Represents an registered user. Each user have an unique ID and an unique username. Contains it's personal data, password and roles assigned. 
When assigned to roles, an authenticated user also gains authorization to endpoints of the API accordingly.


## Test users
To help testing, there are five 'default' or 'root' users with roles assigned to them. 

| Username | Password | Role assigned | User Id |
|:--- |:--- |:--- |:--- |
| admin | admin | Administrator | 1 |
| manager | manager | Manager | 2 |
| stock | stock | Stock | 3 |
| seller | seller | Seller | 4 |
| public | public | *no role* | 5 |

> TIP: make testing quicker by using the auth endpoints for [Quick registering](#authentication-and-authorization) and [Quick authentication](#authentication-and-authorization) to authenticate to the test users.

> All test users and the [default roles](#role) are reset upon authentication to a test user. 

## Operations
### Authentication and Authorization
**Base route: `api/v1/auth/`**

Contains operations to register and authenticate users, returning the token to be passed every request on *Authorization* header.

To help testing the API, there are endpoints that helps creating and authenticating users without needing to pass any data. Upon usage, every premade user data (the default Users and Roles) are reset.

| Operation | Description | URI | Method |
| --- | --- | --- | --- |
| Registration | Registers a new user, authenticating afterwards | /api/v1/auth/register | POST |
| Authentication | Authenticates an existing user and generates the corresponding JWT | /api/v1/auth/authenticate | POST |
| Quick Registration | Registers a new user with randomly generated data, with the choosen Roles assigned, authenticating afterwards. Consuming an external API to get random data. Resets all default Users and Roles upon usage | /api/v1/auth/register/testAccount | POST |
| Quick Authentication | Authenticates to the choosen premade user account. Resets all default Users and Roles upon usage | /api/v1/auth/authenticate/testAccount/{user} | POST |

### Products
**Base route: `api/v1/products/`**

CRUD operations with the products registered. 
When creating a new Product, a Product Stock is created to it. Same goes for deletion. 

| Operation | Description | URI | Method |
| --- | --- | --- | --- |
| Get all products | Finds all products. You can add parameters on the query string to filter the results:<ul><li>MinPrice : `Price` must be greater or equal than this</li><li>MaxPrice : `Price` must be less or equal than this  </li><li>Name : `Name` must contain this string</li><li>Description : `Description` should contain this string</li><li>OnStock : `true` to show only products with at least one unit on stock, `false` to show all products with zero units on stock</li></ul>Result is paginated. Accepts [pagination query string parameters](#pagination)| /api/v1/products | GET |
| Get product by Id | Finds a product by Id | /api/v1/products/{id} | GET |
| Create a new product | Create a new Product and it's respective stock | /api/v1/products |
| Update product | Changes a product entry data | /api/v1/products/{id} | PUT |
| Delete product | Deletes a product and it's product stock data | /api/v1/products/{id} | DELETE |

### Stock
**Base route: `api/v1/stock/`**

Stock related operations. 

| Operation | Description | URI | Method |
| --- | --- | --- | --- |
| Get all product stocks | Finds all product stocks data. You can add parameters on the query string to filter the results:<ul><li>ProductName : `Product` must have a `Name` that contains this string</li><li>QuantityMin : `Quantity` must be greater or equal than this</li><li>QuantityMax : `Quantity` must be less or equal than this</li></ul>Result is paginated. Accepts [pagination query string parameters](#pagination)| /api/v1/stock | GET |
| Get product stock by Id | Finds a product stock data by Id | /api/v1/stock/{id} | GET |
| Get product stock by product Id | Finds a product stock by it's product Id | /api/v1/stock/product/{productId} | GET |
| Update product stock | Edit the data on a product stock. | /api/v1/stock/{id} | PUT |
| Add quantity to product stock | Use to add a quantity of a product on stock. | /api/v1/stock/{id}/add/{quantity} | PUT |
| Remove quantity to product stock | Use to remove a quantity of a product on stock. | /api/v1/stock/{id}/remove/{quantity} | PUT |


### Roles
**Base route: `api/v1/roles/`**

Operations about roles. 

| Operation | Description | URI | Method |
| --- | --- | --- | --- |
| Get all roles | Finds all roles. You can add parameters on the query string to filter the results:<ul><li>Name : `Name` must contain this string</li><li>UserId : select only roles assigned to this user (can be used multiple times on the same query string)</li></ul>Result is paginated. Accepts [pagination query string parameters](#pagination)| /api/v1/roles | GET |
| Create a new role | Create a new role that can be assigned. New roles don't give any extra authorizations | /api/v1/roles | POST |
| Get role by Id | Finds a role by Id | /api/v1/roles/{id} | GET |
| Get role by Name | Finds a role by its *exact* name (case-insensitive) | /api/v1/roles/{roleName} | GET |
| Update product | Changes a product entry data | /api/v1/products/{id} | PUT |
| Delete role | Deletes a role | /api/v1/roles/{id} | DELETE |

To add and remove roles from an user, see [Assign/remove role from user](#user-1)

### Users
**Base route: `api/v1/users/`**

Operations about users data. For registration and authentication, see [Authentication and Authorization](#authentication-and-authorization).

| Operation | Description | URI | Method |
| --- | --- | --- | --- |
| Get all users | Finds all users. You can add parameters on the query string to filter the results:<ul><li>MinDateOfBirth : `DateOfBirth` must be after or at this date</li><li>MaxDateOfBirth : `DateOfBirth` must be before or at this date</li><li>MinAge : user age must be greater or equal than this</li><li>MaxAge : user age must be less or equal than this</li><li>RoleId : select only users assigned to this role (can be used multiple times on the same query string)</li><li>UserName : `UserName` must contain this string</li><li>Name : `FirstName` or `LastName` must contain this string</li></ul>Result is paginated. Accepts [pagination query string parameters](#pagination)| /api/v1/users | GET |
| Get user by Id | Finds an user *detailed* data by Id | /api/v1/users/{id} | GET |
| Update user | Edit an user data | /api/v1/users | PUT |
| Get current user | Finds currently authenticated user *detailed* data | /api/v1/users/current | GET |
| Forced change user password | For Administrators use only. Forcefully changes an user password. | /api/v1/users/{id}/password | PUT |
| Forced reset user password | For Administrators use only. Forcefully resets or change an user password. | /api/v1/users/{id}/password | DELETE |
| Change current user password | Change currently authenticated user password. | /api/v1/users/current/password | PUT |
| Assign role to user | Assigns an user to a role | /api/v1/users/{id}/roles/add/{roleId} | PUT |
| Remove role to user | Dismiss an user from a role | /api/v1/users/{id}/roles/remove/{roleId} | PUT |

## Responses
In addition to the expected success response, there are two more responses you can find:

### Pagination 
Results that can return multiple data are paginated. For that you must include on the URI query string parameters you want for pagination.
- PageNumber : the page number shown from the paginated result
- PageSize : quantity of items each page have. Default is 10, caps at 50.

Paginated results have an extra header `X-Pagination`, with metadata about the query.

- TotalCount : total number of items
- PageSize : item quantity per page
- CurrentPage : page number
- TotalPages : total number of pages
- HasNext : `true` if the result has a next page, `false` if it doesn't
- HasPrevious : `true` if the result has a previous page, `false` if it doesn't

### Error responses
All errors responses type from the API are [Problem Details](https://datatracker.ietf.org/doc/html/rfc7807).
