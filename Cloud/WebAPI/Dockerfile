FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cloud/Cloud.csproj", "Cloud/"]
RUN dotnet restore "Cloud/Cloud.csproj"
COPY . .
WORKDIR "/src/Cloud"
RUN dotnet build "Cloud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cloud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cloud.dll"]
