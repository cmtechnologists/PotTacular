echo "stopping and removing api project containers..."
docker rm -f PottacularApi
docker rm -f pottacular-api_mongo-express_1
docker rm -f pottacular-api_mongo_1
echo "list of remaining running containers..."
docker ps