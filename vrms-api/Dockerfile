FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore packages
COPY ["vrms-api.csproj", "./"]
RUN dotnet restore

# Copy everything else
COPY . .

# Clean obj/bin inside container and publish
RUN dotnet publish "vrms-api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "vrms-api.dll"]