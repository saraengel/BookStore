
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore

RUN dotnet publish BookStoreServer.Server/BookStoreServer.csproj -c Debug  -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build /app/publish .



ENTRYPOINT ["dotnet", "BookStoreServer.dll", "--environment=Development", "--server.urls", "http://0.0.0.0:5000", "--inspect-brk=0.0.0.0:5005"]


EXPOSE 5000

