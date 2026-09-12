call C:\Docker\gcp-server\connect.cmd

docker build .. --file ../vrms-api/Dockerfile -t %REGISTRY%/vrms-api

docker push %REGISTRY%/vrms-api:dev
PAUSE
