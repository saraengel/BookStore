
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .

RUN dotnet restore

RUN dotnet build BookStoreServer.Api.Entities/BookStoreServer.Api.Entities.csproj -c Debug --no-restore
RUN dotnet build BookStoreServer.Api.Shared/BookStoreServer.Api.Shared.csproj -c Debug --no-restore
RUN dotnet build BookStoreServer.Common/BookStoreServer.Common.csproj
RUN dotnet build BookStoreServer.CommonServices/BookStoreServer.CommonServices.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.CommonServices.Interface/BookStoreServer.CommonServices.Interface.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.Entities/BookStoreServer.Entities.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.Model/BookStoreServer.Model.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.Repository/BookStoreServer.Repository.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.Repository.Interfaces/BookStoreServer.Repository.Interfaces.csproj -c Debug  --no-restore
RUN dotnet build BookStoreServer.Server/BookStoreServer.csproj
RUN dotnet build BookStoreServer.Service/BookStoreServer.Service.csproj
RUN dotnet build BookStoreServer.Services.interfaces/BookStoreServer.Services.Interfaces.csproj
RUN dotnet publish BookStoreServer.Server/BookStoreServer.csproj -c Debug  -o /app/publish



FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS publish
WORKDIR /app
COPY --from=build /app/publish .



ENTRYPOINT ["dotnet", "BookStoreServer.dll", "--environment=Development"]


EXPOSE 5000

