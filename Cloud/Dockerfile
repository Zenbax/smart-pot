# Use the official image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 11000

RUN apt-get update && apt-get install -y net-tools  # Install net-tools for netstat
RUN apt-get update && apt-get install -y iproute2  # Install iproute2 for ss, if not already installed

# Define the base image for the SDK (to build and publish the app)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
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

FROM build AS publish
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]
