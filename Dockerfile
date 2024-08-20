# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and project files
COPY *.sln ./
COPY FileProcessor.API/FileProcessor.API.csproj FileProcessor.API/
COPY FileProcessor.Domain/FileProcessor.Domain.csproj FileProcessor.Domain/
COPY FileProcessor.Infrastructure/FileProcessor.Infrastructure.csproj FileProcessor.Infrastructure/
COPY FileProcessor.Application/FileProcessor.Application.csproj FileProcessor.Application/

# Restore dependencies
RUN dotnet restore

# Copy the remaining files
COPY . .

# Build the application
WORKDIR /src/FileProcessor.API
RUN dotnet build -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Use the runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileProcessor.API.dll"]