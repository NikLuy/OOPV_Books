# Use the official .NET 9 runtime as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY ["OOPV_Books.Web/OOPV_Books.Web.csproj", "OOPV_Books.Web/"]
COPY ["OOPV_Books.ServiceDefaults/OOPV_Books.ServiceDefaults.csproj", "OOPV_Books.ServiceDefaults/"]

# Restore dependencies
RUN dotnet restore "OOPV_Books.Web/OOPV_Books.Web.csproj"

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR "/src/OOPV_Books.Web"
RUN dotnet build "OOPV_Books.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "OOPV_Books.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create a non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

ENTRYPOINT ["dotnet", "OOPV_Books.Web.dll"]