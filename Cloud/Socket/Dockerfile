FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Socket/Socket.csproj", "Socket/"]
RUN dotnet restore "Socket/Socket.csproj"
COPY . .
WORKDIR "/src/Socket"
RUN dotnet build "Socket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Socket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Socket.dll"]
