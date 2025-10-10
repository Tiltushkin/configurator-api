# ---- Base runtime image ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

# ---- Build & publish ----
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
WORKDIR /src

# Copy solution and project files first (for better layer caching)
COPY PcConfigurator.sln ./
COPY src/PcConfigurator.Api/PcConfigurator.Api.csproj src/PcConfigurator.Api/PcConfigurator.Api.csproj

RUN dotnet restore "src/PcConfigurator.Api/PcConfigurator.Api.csproj"

# Copy the rest of the source
COPY . .

# Publish
RUN dotnet publish "src/PcConfigurator.Api/PcConfigurator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ---- Final image ----
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Healthcheck (optional): call /health every 30s
HEALTHCHECK --interval=30s --timeout=5s --retries=3 CMD wget -qO- http://localhost:8080/health || exit 1

ENTRYPOINT ["dotnet", "PcConfigurator.Api.dll"]
