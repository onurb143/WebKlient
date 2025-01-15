# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopier projektfiler og gendan afhængigheder
COPY WebKlient.csproj .
RUN dotnet restore WebKlient.csproj

# Kopier al kode og byg applikationen
COPY . .
RUN dotnet publish WebKlient.csproj -c Release -o /publish --property:GenerateStaticWebAssets=true

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopier publicerede filer fra build-stagen
COPY --from=build /publish .

# Kopier HTTPS-certifikatet
COPY https/aspnetapp.pfx /app/https/

# Indstil miljøvariabler til runtime
ENV ASPNETCORE_URLS=http://+:5189;https://+:5199 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    ASPNETCORE_Kestrel__Endpoints__Https__Certificate__Path=/app/https/aspnetapp.pfx \
    ASPNETCORE_Kestrel__Endpoints__Https__Certificate__Password=Test1234!

# Start applikationen
ENTRYPOINT ["dotnet", "WebKlient.dll"]
