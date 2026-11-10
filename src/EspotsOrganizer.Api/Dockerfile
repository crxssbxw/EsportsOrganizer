# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["EsportsOrganizer.Api.csproj", "./"]
RUN dotnet restore "EsportsOrganizer.Api.csproj"

COPY . .
RUN dotnet publish "EsportsOrganizer.Api.csproj" -c Release -o /app/publis

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "EsportsOrganizer.Api.dll"]
