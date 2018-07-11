# Build stage TODO: Needed?
FROM microsoft/aspnetcore-build:2 AS build

# set working directory
WORKDIR /app

# Restore
COPY src/src.csproj ./src/
RUN dotnet restore src/src.csproj
COPY run/run.csproj ./run/
RUN dotnet restore run/run.csproj
COPY test/test.csproj ./test/
RUN dotnet restore test/test.csproj

# Copy Source
COPY . .

# Test
ENV TEAMCITY_PROJECT_NAME=fake
#RUN dotnet test test/test.csproj

# Publish
RUN dotnet publish src/src.csproj -o /obj
RUN dotnet publish run/run.csproj -o /obj

# Runtime stage
FROM microsoft/aspnetcore:2

# set working directory
WORKDIR /app

# Copy compiled binaries
COPY --from=build /obj ./bin

# Run command is in the docker-compose.yml file