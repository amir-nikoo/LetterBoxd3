# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy solution file and all project folders into container
COPY LetterBoxd3Solution.sln ./
COPY LetterBoxd3 ./LetterBoxd3
COPY LetterBoxdContext ./LetterBoxdContext
COPY LetterBoxdDomain ./LetterBoxdDomain
COPY LetterBoxd3/Configurations ./LetterBoxd3/Configurations

# Restore all projects referenced by the solution
RUN dotnet restore LetterBoxd3Solution.sln

# Publish only the main web app project (output to /app/publish)
RUN dotnet publish LetterBoxd3/LetterBoxd3.csproj -c Release -o /app/publish

# Use the official .NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

# Expose port 8080 (Render uses this by default)
EXPOSE 8080

# Set environment variable for the port
ENV ASPNETCORE_URLS=http://+:8080

# Run the app with correct dll name (case-sensitive)
ENTRYPOINT ["dotnet", "LetterBoxd3.dll"]
