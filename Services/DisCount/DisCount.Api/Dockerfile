# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/DisCount/DisCount.Api/DisCount.Api.csproj", "Services/DisCount/DisCount.Api/"]
COPY ["Services/DisCount/DisCount.Application/DisCount.Application.csproj", "Services/DisCount/DisCount.Application/"]
COPY ["Services/DisCount/DisCount.Core/DisCount.Core.csproj", "Services/DisCount/DisCount.Core/"]
COPY ["Services/DisCount/DisCount.InfraStructure/DisCount.InfraStructure.csproj", "Services/DisCount/DisCount.InfraStructure/"]
RUN dotnet restore "./Services/DisCount/DisCount.Api/DisCount.Api.csproj"
COPY . .
WORKDIR "/src/Services/DisCount/DisCount.Api"
RUN dotnet build "./DisCount.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DisCount.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DisCount.Api.dll"]