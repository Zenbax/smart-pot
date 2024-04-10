# Use the official .NET 7 SDK image to build the application projects.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory in the container to /src.
WORKDIR /src

# Copy the CSPROJ files and restore any dependencies (via NUGET)
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Socket/Socket.csproj", "Socket/"]

# Restore the NuGet packages for each project.
RUN dotnet restore "WebAPI/WebAPI.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Socket/Socket.csproj"

# Copy the source files for each project into the container.
COPY . .

# Build projects individually
RUN dotnet build "WebAPI/WebAPI.csproj" -c Release --no-restore
RUN dotnet build "Socket/Socket.csproj" -c Release --no-restore

# Publish the WebAPI project - this assumes WebAPI is the startup project
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish

# Use the official .NET runtime image to create a runtime container.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final

# Set the working directory in the container to /app.
WORKDIR /app

# Copy the published application from the build container to the final container.
COPY --from=build /app/publish .

# Set environment variables for the runtime.
ENV ASPNETCORE_URLS=http://+:80

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "WebAPI.dll"]
