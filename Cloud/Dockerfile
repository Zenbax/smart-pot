# Use the base image with .NET runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443 11000

# Install Supervisord
RUN apt-get update && apt-get install -y supervisor
# Setup Supervisord log and configuration directories
RUN mkdir -p /var/log/supervisor /etc/supervisor/conf.d

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy and restore all projects
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Socket/Socket.csproj", "Socket/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Socket/Socket.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "WebAPI/WebAPI.csproj" -c Release -o /app/build
RUN dotnet build "Socket/Socket.csproj" -c Release -o /app/build

# Publish the applications
FROM build AS publish
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish
RUN dotnet publish "Socket/Socket.csproj" -c Release -o /app/publish

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf
ENTRYPOINT ["/usr/bin/supervisord", "-c", "/etc/supervisor/conf.d/supervisord.conf"]
