FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the current directory (.) to the working directory
COPY . .

# Restore NuGet packages and build the application
RUN dotnet restore
RUN dotnet build -c Release

# Run the application
CMD ["dotnet", "run"]