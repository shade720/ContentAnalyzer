# docker run --name DataCollectionService -d -it -p 5051:80 -p 5061:443 -e POSTGRES_CONNECTION_STRING="Host=host.docker.internal;Port=32768;Database=ContentAnalyzerDatabase;Username=postgres;Password=postgrespw" data-collection-service
# docker build -t unh0ly/data-collection-service:latest .
# docker push unh0ly/data-collection-service

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DataCollection/DataCollectionService.Grpc.UI/DataCollectionService.Grpc.UI.csproj", "DataCollection/DataCollectionService.Grpc.UI/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["DataCollection/DataCollectionService.Application/DataCollectionService.Application.csproj", "DataCollection/DataCollectionService.Application/"]
COPY ["DataCollection/DataCollectionService.Domain/DataCollectionService.Domain.csproj", "DataCollection/DataCollectionService.Domain/"]
COPY ["DataCollection/DataCollectionService.Infrastructure/DataCollectionService.Infrastructure.csproj", "DataCollection/DataCollectionService.Infrastructure/"]
RUN dotnet restore "DataCollection/DataCollectionService.Grpc.UI/DataCollectionService.Grpc.UI.csproj"
COPY . .
WORKDIR "/src/DataCollection/DataCollectionService.Grpc.UI"
RUN dotnet build "DataCollectionService.Grpc.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataCollectionService.Grpc.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataCollectionService.Grpc.UI.dll"]