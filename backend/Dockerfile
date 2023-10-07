# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy the project files and restore dependencies
COPY . ./
RUN dotnet restore

# Build the application
RUN cd IntegrifyLibrary.Infrastructure/ && dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/IntegrifyLibrary.Infrastructure/out .
ENTRYPOINT ["dotnet", "IntegrifyLibrary.Infrastructure.dll"]
