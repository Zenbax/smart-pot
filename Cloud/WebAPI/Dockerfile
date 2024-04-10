# Use the official image as a parent image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Set the working directory.
WORKDIR /app

# Expose ports.
EXPOSE 6969
EXPOSE 44362

# Set environment variables.
ENV ASPNETCORE_URLS=http://+:6969 \
    ASPNETCORE_ENVIRONMENT=Development \
    ASPNETCORE_Kestrel__Certificates__Default__Password="123" \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Use SDK image to build the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers.
COPY ["Cloud.csproj", "./"]
RUN dotnet restore "Cloud.csproj"

# Copy everything else and build website.
COPY . .
RUN dotnet build "Cloud.csproj" -c Release -o /app/build

# Publish the application.
FROM build AS publish
RUN dotnet publish "Cloud.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage/image.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Run the application.
ENTRYPOINT ["dotnet", "Cloud.dll"]
