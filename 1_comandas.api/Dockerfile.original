# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["1_comandas.api/comandas.api.csproj", "1_comandas.api/"]
COPY ["2_comandas.Domain/comandas.Domain.csproj", "2_comandas.Domain/"]
COPY ["3_comandas.Services/comandas.Services.csproj", "3_comandas.Services/"]
COPY ["4_comandas.Data/comandas.Data.csproj", "4_comandas.Data/"]
COPY ["5_comandas.Shared/comandas.Shared.csproj", "5_comandas.Shared/"]
RUN dotnet restore "./1_comandas.api/comandas.api.csproj"
COPY . .
WORKDIR "/src/1_comandas.api"
RUN dotnet build "./comandas.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./comandas.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "comandas.api.dll"]