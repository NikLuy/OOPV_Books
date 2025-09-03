@echo off

echo Building OOPV Books Application...

REM Build the Docker images
echo Building API Service image...
docker build -f OOPV_Books.ApiService/Dockerfile -t oopv-books-api:latest .

echo Building Web Application image...
docker build -f OOPV_Books.Web/Dockerfile -t oopv-books-web:latest .

echo Build completed successfully!

REM Optional: Run the application
set /p choice="Do you want to start the application with docker-compose? (y/n): "
if "%choice%"=="y" (
    echo Starting application...
    docker-compose up -d
    echo Application started!
    echo API is available at: http://localhost:5001
    echo Web App is available at: http://localhost:5003
    echo OpenAPI documentation: http://localhost:5001/openapi/v1.json
)

pause