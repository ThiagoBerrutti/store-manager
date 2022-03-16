# Store API
Basic store api for stock management 

## Live demo


## Running with Docker containers
### with docker compose 
1- on terminal, go to project root folder, where the file 'docker-compose.yaml' is
1- enter the command `docker compose up`,
2- wait for it to completely load

Enter http://localhost:5000/swagger, should work

### executing pre-made shell scripts
1- enter folder './setup'
2- execute:
- 1-docker-network
- 2-docker-sqlserver
- 3-docker-store-api

enter http://localhost:5000/swagger, should work

## running from source 
1- clone the git repository `git clone https://github.com/ThiagoBerrutti/sales-api.git`
2- enter folder './setup'
3- execute '2-docker-sqlserver'
OR
on terminal, enter the command `
docker run -d \
-p 1401:1433 \
--name sqlserver \
--net store-network \
-e ACCEPT_EULA=Y \
-e SA_PASSWORD=1q2w3e4r@#$ \
mcr.microsoft.com/mssql/server:latest`

4- run the cloned project

Enter http://localhost:5000/swagger, should work



