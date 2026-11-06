call C:\Docker\vbsxr\vbsxr-cmd.cmd

docker pull %REGISTRY%/vrms-api

docker-compose -f api.yml -p vrms up -d
TIMEOUT 15
