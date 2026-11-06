call C:\Docker\vbsxr\vbsxr-cmd.cmd

docker build --no-cache ^
  --network=host ^
  -f ..\vrms-api\Dockerfile ^
  -t %REGISTRY%/vrms-api:dev ^
  ..\vrms-api

docker push %REGISTRY%/vrms-api:dev
PAUSE
