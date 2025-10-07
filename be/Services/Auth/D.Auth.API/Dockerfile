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
COPY ["Services/Auth/D.Auth.API/D.Auth.API.csproj", "Services/Auth/D.Auth.API/"]
COPY ["Library/D.ApplicationBase/D.ApplicationBase.csproj", "Library/D.ApplicationBase/"]
COPY ["Library/D.DomainBase/D.DomainBase.csproj", "Library/D.DomainBase/"]
COPY ["Library/D.ControllerBase/D.ControllerBase.csproj", "Library/D.ControllerBase/"]
COPY ["Library/D.InfrastructureBase/D.InfrastructureBase.csproj", "Library/D.InfrastructureBase/"]
COPY ["Services/Shared/d.Shared.Permission/d.Shared.Permission.csproj", "Services/Shared/d.Shared.Permission/"]
COPY ["Services/Auth/D.Auth.Application/D.Auth.Application.csproj", "Services/Auth/D.Auth.Application/"]
COPY ["Services/Auth/D.Auth.Domain/D.Auth.Domain.csproj", "Services/Auth/D.Auth.Domain/"]
COPY ["Services/Auth/D.Auth.Infrastructure/D.Auth.Infrastructure.csproj", "Services/Auth/D.Auth.Infrastructure/"]
RUN dotnet restore "./Services/Auth/D.Auth.API/D.Auth.API.csproj"
COPY . .
WORKDIR "/src/Services/Auth/D.Auth.API"
RUN dotnet build "./D.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./D.Auth.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D.Auth.API.dll"]