#!/bin/bash

echo "=== OOPV Books - Docker Deployment Verification ==="
echo ""

# Check if Docker is running
if ! docker info > /dev/null 2>&1; then
    echo "? Docker is not running. Please start Docker Desktop and try again."
    exit 1
fi

echo "? Docker is running"

# Build the images
echo ""
echo "?? Building Docker images..."
echo ""

echo "Building API Service..."
if docker build -f OOPV_Books.ApiService/Dockerfile -t oopv-books-api:latest .; then
    echo "? API Service image built successfully"
else
    echo "? Failed to build API Service image"
    exit 1
fi

echo ""
echo "Building Web Application..."
if docker build -f OOPV_Books.Web/Dockerfile -t oopv-books-web:latest .; then
    echo "? Web Application image built successfully"
else
    echo "? Failed to build Web Application image"
    exit 1
fi

echo ""
echo "?? All Docker images built successfully!"
echo ""
echo "To start the application:"
echo "  docker-compose up -d"
echo ""
echo "To access the application:"
echo "  Web App: http://localhost:5003"
echo "  API:     http://localhost:5001"
echo ""
echo "To stop the application:"
echo "  docker-compose down"