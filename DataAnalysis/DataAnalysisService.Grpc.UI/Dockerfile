# docker run --name DataAnalysisService -d -it -p 5052:80 -p 5062:443 -e POSTGRES_CONNECTION_STRING="Host=host.docker.internal;Port=32768;Database=ContentAnalyzerDatabase;Username=postgres;Password=postgrespw" unh0ly/data-analysis-service
# docker build -f DataAnalysis\DataAnalysisService.Grpc.UI\Dockerfile -t unh0ly/data-analysis-service:latest .
# docker push unh0ly/data-analysis-service

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DataAnalysis/DataAnalysisService.Grpc.UI/DataAnalysisService.Grpc.UI.csproj", "DataAnalysis/DataAnalysisService.Grpc.UI/"]
COPY ["DataAnalysis/DataAnalysisService.Application/DataAnalysisService.Application.csproj", "DataAnalysis/DataAnalysisService.Application/"]
COPY ["DataAnalysis/DataAnalysisService.Domain/DataAnalysisService.Domain.csproj", "DataAnalysis/DataAnalysisService.Domain/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["DataAnalysis/DataAnalysisService.Infrastructure/DataAnalysisService.Infrastructure.csproj", "DataAnalysis/DataAnalysisService.Infrastructure/"]
RUN dotnet restore "DataAnalysis/DataAnalysisService.Grpc.UI/DataAnalysisService.Grpc.UI.csproj"
COPY . .
WORKDIR "/src/DataAnalysis/DataAnalysisService.Grpc.UI"
RUN dotnet build "DataAnalysisService.Grpc.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DataAnalysisService.Grpc.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAnalysisService.Grpc.UI.dll"]