docker stop store-api
docker rm store-api
docker run -d -p 5000:80 \
--name store-api \
--net store-network \
--restart on-failure:10  \
-e PORT=5000 \
-e ASPNETCORE_ENVIRONMENT=Development \
-e StoreDbSQLServerConnectionStringSettings__DataSource=sqlserver,1433 \
thiagoberrutti/store-api
