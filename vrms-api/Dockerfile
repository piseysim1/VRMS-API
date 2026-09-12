# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 80
EXPOSE 443

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["vrms-api.csproj", "."]
RUN dotnet restore "./vrms-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./vrms-api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./vrms-api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production
FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vrms-api.dll"]