# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["WebKlient.csproj", "./"]
RUN dotnet restore "WebKlient.csproj"

# Copy all files and set the correct working directory
COPY . .
WORKDIR "/src"

# Build the application
RUN dotnet build "WebKlient.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
WORKDIR "/src"
RUN dotnet publish "WebKlient.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Copy only the published output to the runtime container
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:5189
EXPOSE 5189

ENTRYPOINT ["dotnet", "WebKlient.dll"]