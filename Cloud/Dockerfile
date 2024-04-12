# Define the base image for the SDK (to build and publish the app)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files and restore NuGet packages separately to utilize Docker cache
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Socket/Socket.csproj", "Socket/"]
RUN dotnet restore "WebAPI/WebAPI.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Socket/Socket.csproj"

# Copy the rest of the source code
COPY . .

# Build the projects
RUN dotnet build "WebAPI/WebAPI.csproj" -c Release -o /app/build
RUN dotnet build "Socket/Socket.csproj" -c Release -o /app/build

# Publish the WebAPI project
FROM build AS publish
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish

# Define the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app

# Copy the published app from the 'publish' stage
COPY --from=publish /app/publish .

# Expose the port the app runs on and set the entry point
EXPOSE 6969
ENTRYPOINT ["dotnet", "WebAPI.dll"]
