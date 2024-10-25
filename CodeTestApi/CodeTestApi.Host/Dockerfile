# Etapa 1: Construir el proyecto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo .sln y los archivos .csproj
COPY *.sln ./
COPY CodeTestApi.Host/*.csproj ./CodeTestApi.Host/
COPY CodeTestApi.Application/*.csproj ./CodeTestApi.Application/
COPY CodeTestApi.Infrastructure/*.csproj ./CodeTestApi.Infrastructure/
COPY CodeTestApi.Domain/*.csproj ./CodeTestApi.Domain/

# Restaurar las dependencias
RUN dotnet restore ./CodeTestApi.Host/CodeTestApi.Host.csproj

# Copiar el código fuente restante y compilar
COPY . .
WORKDIR /src/CodeTestApi.Host
RUN dotnet publish -c Release -o /app

# Etapa 2: Ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CodeTestApi.Host.dll"]
