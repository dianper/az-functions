# Use the official .NET 8 SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the project files
COPY MyFunctionApp.sln ./
COPY MyFunctionApp.Function/MyFunctionApp.csproj MyFunctionApp.Function/

# Copy the test project files
COPY MyFunctionApp.Function.Tests/MyFunctionApp.Function.Tests.csproj MyFunctionApp.Function.Tests/
RUN dotnet restore MyFunctionApp.sln

# Copy the entire directory and build the application
COPY . ./
RUN dotnet publish MyFunctionApp.Function/MyFunctionApp.csproj -c Release -o out

# Use the Azure Functions .NET 8 runtime as the base image
FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0

# Set the working directory inside the container
WORKDIR /home/site/wwwroot

# Copy the published output from the build environment
COPY --from=build-env /app/out .

# Expose the function port (default port for Azure Functions is 80)
EXPOSE 80
