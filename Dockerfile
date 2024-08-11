FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["AmigosConCola.Core/AmigosConCola.Core.csproj", "AmigosConCola.Core/"]
COPY ["AmigosConCola.WebApi/AmigosConCola.WebApi.csproj", "AmigosConCola.WebApi/"]
RUN dotnet restore 'AmigosConCola.WebApi/AmigosConCola.WebApi.csproj'

COPY ["AmigosConCola.Core/", "AmigosConCola.Core/"]
COPY ["AmigosConCola.WebApi/", "AmigosConCola.WebApi/"]
RUN dotnet build 'AmigosConCola.WebApi/AmigosConCola.WebApi.csproj' -c Release -o /app/build

FROM build AS publish
WORKDIR /src/AmigosConCola.WebApi/
RUN dotnet publish 'AmigosConCola.WebApi.csproj' -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0

RUN apt-get update && apt-get install -y libxml2-dev

WORKDIR /app

EXPOSE 5000

RUN mkdir -p /app/Images

COPY --from=build /app/build /app/build

COPY --from=publish /app/publish .
COPY AmigosConCola.WebApi/appsettings.json .

ENTRYPOINT ["dotnet", "AmigosConCola.WebApi.dll"]
