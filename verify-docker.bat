@echo off

echo === OOPV Books - Docker Deployment Verification ===
echo.

REM Check if Docker is running
docker info >nul 2>&1
if errorlevel 1 (
    echo X Docker is not running. Please start Docker Desktop and try again.
    pause
    exit /b 1
)

echo ? Docker is running

REM Build the images
echo.
echo Building Docker images...
echo.

echo Building API Service...
docker build -f OOPV_Books.ApiService/Dockerfile -t oopv-books-api:latest .
if errorlevel 1 (
    echo X Failed to build API Service image
    pause
    exit /b 1
) else (
    echo ? API Service image built successfully
)

echo.
echo Building Web Application...
docker build -f OOPV_Books.Web/Dockerfile -t oopv-books-web:latest .
if errorlevel 1 (
    echo X Failed to build Web Application image
    pause
    exit /b 1
) else (
    echo ? Web Application image built successfully
)

echo.
echo All Docker images built successfully!
echo.
echo To start the application:
echo   docker-compose up -d
echo.
echo To access the application:
echo   Web App: http://localhost:5003
echo   API:     http://localhost:5001
echo.
echo To stop the application:
echo   docker-compose down

pause