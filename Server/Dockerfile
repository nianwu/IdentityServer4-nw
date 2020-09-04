# FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
FROM 192.168.8.114:5000/netcore/runtime:3.1 AS base
WORKDIR /app

# FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM 192.168.8.114:5000/netcore/sdk:3.1 AS build
WORKDIR /src
# COPY nuget.config .

COPY utils/utils.csproj utils/utils.csproj
COPY Server/Server.csproj Server/Server.csproj
RUN dotnet restore "Server/Server.csproj"

COPY utils/ utils/
COPY Server/ Server/
WORKDIR "/src/Server"
RUN dotnet build "Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Server.dll"]
