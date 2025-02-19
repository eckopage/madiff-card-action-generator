# Use the .NET SDK for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project files and restore dependencies
COPY ["src/CardService.csproj", "CardService/"]
WORKDIR /src/CardService
RUN dotnet restore

# Copy the entire source code and build the application
COPY src/ .
RUN dotnet publish -c Release -o /app/publish

# Use a lightweight .NET runtime for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose necessary ports
EXPOSE 80
EXPOSE 443

# Start the application
ENTRYPOINT ["dotnet", "CardService.dll"]
