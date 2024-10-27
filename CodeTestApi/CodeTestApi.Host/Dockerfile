FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln ./
COPY CodeTestApi.Host/*.csproj ./CodeTestApi.Host/
COPY CodeTestApi.Application/*.csproj ./CodeTestApi.Application/
COPY CodeTestApi.Infrastructure/*.csproj ./CodeTestApi.Infrastructure/
COPY CodeTestApi.Domain/*.csproj ./CodeTestApi.Domain/

RUN dotnet restore ./CodeTestApi.Host/CodeTestApi.Host.csproj

COPY . .
WORKDIR /src/CodeTestApi.Host
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "CodeTestApi.Host.dll"]
