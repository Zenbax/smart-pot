# Use the official .NET 7 SDK image to build the application projects.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory in the container to /src.
WORKDIR /src

# Copy the CSPROJ files and restore any dependencies (via NUGET)
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Socket/Socket.csproj", "Socket/"]

# Restore the NuGet packages for each project.
RUN dotnet restore "WebAPI/WebAPI.csproj" \
 && dotnet restore "Domain/Domain.csproj" \
 && dotnet restore "Socket/Socket.csproj"

# Copy the source files for each project into the container.
COPY . .

# Publish the WebAPI project - this assumes WebAPI is the startup project
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish --no-restore

# Use the official .NET runtime image to create a runtime container.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

# Set the working directory in the container to /app.
WORKDIR /app

# Copy the published application from the build container to the final container.
COPY --from=build /app/publish .

# Set environment variables for the runtime.
ENV ASPNETCORE_URLS=http://+:6969;http://+:44362 \
    ASPNETCORE_ENVIRONMENT=Development \
    ASPNETCORE_Kestrel__Certificates__Default__Password="123" \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

# Expose the ports that the applications listen on.
EXPOSE 6969
EXPOSE 44362

# Start the application using the "dotnet" command.
ENTRYPOINT ["dotnet", "WebAPI.dll"]
