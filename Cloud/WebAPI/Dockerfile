FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 6969
EXPOSE 44362

ENV ASPNETCORE_URLS=http://+:6969
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="123"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Cloud.csproj", "./"]
RUN dotnet restore "Cloud.csproj"
COPY . .
WORKDIR "./"
RUN dotnet build "Cloud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cloud.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cloud.dll"]
