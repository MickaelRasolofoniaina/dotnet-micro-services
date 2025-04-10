# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the solution file
COPY ../../*.sln ./

# Copy the CatalogAPI project files
COPY ./*.csproj ./CatalogAPI/

# Copy the BuildingBlocks project files
COPY ../../../../../../BuildingBlocks/BuildingBlocks/*.csproj ./BuildingBlocks/

# Restore dependencies for the entire solution
RUN dotnet restore

# Copy the entire source code
COPY ../../ .

# Build the application
RUN dotnet publish ./CatalogAPI/CatalogAPI.csproj -c Release -o /publish

# Use the official .NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published files from the build stage
COPY --from=build /publish .

# Expose the ports defined in launchSettings.json
EXPOSE 5050
EXPOSE 5000

# Set the entry point for the container
ENTRYPOINT ["dotnet", "CatalogAPI.dll"]