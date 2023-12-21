# docker run --name ContentAnalyzer.Gateway -d -it -p 5050:80 -p 5060:443 -e POSTGRES_CONNECTION_STRING="Host=host.docker.internal;Port=32768;Database=ContentAnalyzerDatabase;Username=postgres;Password=postgrespw" -e DATA_ANALYSIS_SERVICE_HOST="http://host.docker.internal:5052" -e DATA_COLLECTION_SERVICE_HOST="http://host.docker.internal:5051" content-analyzer-gateway
# docker build -f ContentAnalyzer.Gateway\Dockerfile -t unh0ly/content-analyzer-gateway:latest .
# docker push unh0ly/content-analyzer-gateway

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ContentAnalyzer.Gateway/ContentAnalyzer.Gateway.csproj", "ContentAnalyzer.Gateway/"]
RUN dotnet restore "ContentAnalyzer.Gateway/ContentAnalyzer.Gateway.csproj"
COPY . .
WORKDIR "/src/ContentAnalyzer.Gateway"
RUN dotnet build "ContentAnalyzer.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContentAnalyzer.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContentAnalyzer.Gateway.dll"]