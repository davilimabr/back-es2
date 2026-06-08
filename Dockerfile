# Build em multiplos estagios para gerar uma imagem final enxuta.

# Estagio de build: restaura, compila e publica a API.
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia apenas os csproj primeiro para aproveitar o cache de restore.
COPY src/CardioTrack.Api/CardioTrack.Api.csproj src/CardioTrack.Api/
COPY src/CardioTrack.Application/CardioTrack.Application.csproj src/CardioTrack.Application/
COPY src/CardioTrack.Domain/CardioTrack.Domain.csproj src/CardioTrack.Domain/
COPY src/CardioTrack.Infrastructure/CardioTrack.Infrastructure.csproj src/CardioTrack.Infrastructure/
RUN dotnet restore src/CardioTrack.Api/CardioTrack.Api.csproj

# Copia o restante do codigo e publica em Release.
COPY src/ src/
RUN dotnet publish src/CardioTrack.Api/CardioTrack.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Estagio final: apenas o runtime do ASP.NET Core.
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Kestrel escuta na porta 8080 (HTTP) dentro do container.
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production

# Executa como usuario nao-root (presente nas imagens .NET).
USER $APP_UID

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CardioTrack.Api.dll"]
