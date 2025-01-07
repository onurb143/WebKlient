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

# Konfigurer miljøvariabler for HTTPS
ENV ASPNETCORE_URLS="http://+:5189;https://+:5199"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/https/aspnetapp.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=Test1234!

# Eksponer porte til HTTP og HTTPS
EXPOSE 5189
EXPOSE 5199

# Start applikationen
ENTRYPOINT ["dotnet", "WebKlient.dll"]
