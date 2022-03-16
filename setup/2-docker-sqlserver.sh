docker stop sqlserver
docker rm sqlserver
docker run -d \
-p 1401:1433 \
--name sqlserver \
--net store-network \
-e ACCEPT_EULA=Y \
-e SA_PASSWORD=1q2w3e4r@#$ \
mcr.microsoft.com/mssql/server:latest