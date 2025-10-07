# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Core/D.Core.API/D.Core.API.csproj", "Services/Core/D.Core.API/"]
COPY ["Library/D.ControllerBase/D.ControllerBase.csproj", "Library/D.ControllerBase/"]
COPY ["Library/D.InfrastructureBase/D.InfrastructureBase.csproj", "Library/D.InfrastructureBase/"]
COPY ["Library/D.DomainBase/D.DomainBase.csproj", "Library/D.DomainBase/"]
COPY ["Services/Shared/d.Shared.Permission/d.Shared.Permission.csproj", "Services/Shared/d.Shared.Permission/"]
COPY ["Services/Core/D.Core.Application/D.Core.Application.csproj", "Services/Core/D.Core.Application/"]
COPY ["Library/D.ApplicationBase/D.ApplicationBase.csproj", "Library/D.ApplicationBase/"]
COPY ["Services/Core/D.Core.Domain/D.Core.Domain.csproj", "Services/Core/D.Core.Domain/"]
COPY ["Services/Core/D.Core.Infrastructure/D.Core.Infrastructure.csproj", "Services/Core/D.Core.Infrastructure/"]
RUN dotnet restore "./Services/Core/D.Core.API/D.Core.API.csproj"
COPY . .
WORKDIR "/src/Services/Core/D.Core.API"
RUN dotnet build "./D.Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./D.Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "D.Core.API.dll"]