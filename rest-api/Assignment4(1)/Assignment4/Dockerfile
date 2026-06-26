FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY IMDBAPI.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish IMDBAPI.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish ./
EXPOSE 5000

ENTRYPOINT ["dotnet", "IMDBAPI.dll"]
