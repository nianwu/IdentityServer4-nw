# FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
FROM 192.168.8.114:5000/netcore/runtime:3.1 AS base
WORKDIR /app

# FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
FROM 192.168.8.114:5000/netcore/sdk:3.1 AS build
WORKDIR /src
# COPY nuget.config .

COPY utils/utils.csproj utils/utils.csproj
COPY Utils.Is4/Utils.Is4.csproj Utils.Is4/Utils.Is4.csproj
COPY Server/Server.csproj Server/Server.csproj
COPY Api/Api.csproj Api/Api.csproj
RUN dotnet restore "Api/Api.csproj"

COPY utils/ utils/
COPY Utils.Is4/ Utils.Is4/
COPY Server/ Server/
COPY Api/ Api/
WORKDIR "/src/Api"
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
