FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files separately to improve layer caching
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Socket/Socket.csproj", "Socket/"]

# Restore all dependencies
RUN dotnet restore "WebAPI/WebAPI.csproj"
RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Socket/Socket.csproj"

# Copy the source code and build the application
COPY . .
RUN dotnet build "WebAPI/WebAPI.csproj" -c Release -o /app/build
RUN dotnet build "Socket/Socket.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expose the ports that the applications listen on.
EXPOSE 6969
EXPOSE 44362

# Start the application using the "dotnet" command.
ENTRYPOINT ["dotnet", "WebAPI.dll"]
