call C:\Docker\gcp-server\connect.cmd

docker pull %REGISTRY%/vrms-api

docker-compose -f api.yml -p vrms up -d
TIMEOUT 15
