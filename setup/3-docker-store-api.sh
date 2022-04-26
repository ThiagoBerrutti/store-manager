docker stop store-api
docker rm store-api
docker run -d -p 5000:5000 \
--name store-api \
--net store-network \
--restart unless-stopped  \
-e PORT=5000 \
-e StoreDbSQLServerConnectionStringSettings__DataSource=sqlserver,1433 \
thiagoberrutti/store-api