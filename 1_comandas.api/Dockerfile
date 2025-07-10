# Fase base: ambiente de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Instala utilitários ping e netcat para debugging
RUN apt-get update && apt-get install -y iputils-ping netcat-openbsd && rm -rf /var/lib/apt/lists/*

# Fase build: compilar o projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["1_comandas.api/comandas.api.csproj", "1_comandas.api/"]
COPY ["2_comandas.Domain/comandas.Domain.csproj", "2_comandas.Domain/"]
COPY ["5_comandas.Shared/comandas.Shared.csproj", "5_comandas.Shared/"]
COPY ["3_comandas.Services/comandas.Services.csproj", "3_comandas.Services/"]
COPY ["4_comandas.Data/comandas.Data.csproj", "4_comandas.Data/"]
RUN dotnet restore "./1_comandas.api/comandas.api.csproj"
COPY . .
WORKDIR "/src/1_comandas.api"
RUN dotnet build "./comandas.api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase publish: publicar o projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./comandas.api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Fase final: runtime + arquivos publicados + ferramentas adicionais
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "comandas.api.dll"]
