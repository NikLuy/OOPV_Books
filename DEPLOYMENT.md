# OOPV Books - Deployment Guide

This document provides comprehensive instructions for deploying the OOPV Books application in various environments.

## ?? Quick Start

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop) installed and running
- [Git](https://git-scm.com/) for cloning the repository

### Local Deployment with Docker

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd OOPV_Books
   ```

2. **Deploy using Docker Compose** (Recommended)
   ```bash
   # Start the application
   docker-compose up -d --build
   
   # View logs
   docker-compose logs -f
   
   # Stop the application
   docker-compose down
   ```

3. **Access the application**
   - ?? **Web Application**: http://localhost:5003
   - ?? **API Service**: http://localhost:5001
   - ?? **API Documentation**: http://localhost:5001/openapi/v1.json

## ?? Container Architecture

The application consists of two main services:

### API Service (`oopv-books-api`)
- **Port**: 5001:8080 (Host:Container)
- **Technology**: ASP.NET Core Web API (.NET 9)
- **Features**: REST API for books and reviews management
- **Health Check**: Available at `/health`

### Web Application (`oopv-books-web`)
- **Port**: 5003:8080 (Host:Container)
- **Technology**: Blazor Server (.NET 9)
- **Features**: Interactive web interface
- **Health Check**: Available at `/health`

## ??? Build Scripts

### Windows
```cmd
# Verify Docker and build images
verify-docker.bat

# Build and optionally start
build-docker.bat
```

### Linux/macOS
```bash
# Make scripts executable
chmod +x verify-docker.sh build-docker.sh

# Verify Docker and build images
./verify-docker.sh

# Build and optionally start
./build-docker.sh
```

## ?? Environment Configuration

### Development Environment
Use `docker-compose.override.yml` for development-specific settings:
- Environment variables set to `Development`
- Volume mounts for hot reload (if needed)
- Debug-friendly configurations

### Production Environment
For production deployment, ensure:
- Set `ASPNETCORE_ENVIRONMENT=Production`
- Configure proper HTTPS certificates
- Set up reverse proxy (nginx, Traefik, etc.)
- Configure logging and monitoring
- Set up persistent storage if needed

## ?? Cloud Deployment Options

### Azure Container Instances (ACI)

1. **Prerequisites**
   - Azure CLI installed and logged in
   - Resource group created

2. **Deploy API Service**
   ```bash
   az container create \
     --resource-group myResourceGroup \
     --name oopv-books-api \
     --image ghcr.io/yourusername/oopv-books-api:latest \
     --port 80 \
     --environment-variables ASPNETCORE_ENVIRONMENT=Production \
     --cpu 1 --memory 1
   ```

3. **Deploy Web Application**
   ```bash
   az container create \
     --resource-group myResourceGroup \
     --name oopv-books-web \
     --image ghcr.io/yourusername/oopv-books-web:latest \
     --port 80 \
     --environment-variables ASPNETCORE_ENVIRONMENT=Production ApiService__BaseUrl=http://api-service-ip \
     --cpu 1 --memory 1
   ```

### Azure Container Apps

1. **Create Container App Environment**
   ```bash
   az containerapp env create \
     --name oopv-books-env \
     --resource-group myResourceGroup \
     --location eastus
   ```

2. **Deploy Services**
   ```bash
   # API Service
   az containerapp create \
     --name oopv-books-api \
     --resource-group myResourceGroup \
     --environment oopv-books-env \
     --image ghcr.io/yourusername/oopv-books-api:latest \
     --target-port 8080 \
     --ingress external

   # Web Application
   az containerapp create \
     --name oopv-books-web \
     --resource-group myResourceGroup \
     --environment oopv-books-env \
     --image ghcr.io/yourusername/oopv-books-web:latest \
     --target-port 8080 \
     --ingress external \
     --env-vars ApiService__BaseUrl=https://api-app-url
   ```

### AWS ECS/Fargate

1. **Create Task Definitions**
   - Define tasks for both API and Web services
   - Configure networking and security groups
   - Set environment variables

2. **Deploy Services**
   - Create ECS cluster
   - Deploy services using Fargate
   - Configure Application Load Balancer

### Docker Swarm

1. **Initialize Swarm**
   ```bash
   docker swarm init
   ```

2. **Deploy Stack**
   ```bash
   docker stack deploy -c docker-compose.yml oopv-books
   ```

### Kubernetes

1. **Create Deployment Manifests**
   ```yaml
   # api-deployment.yaml
   apiVersion: apps/v1
   kind: Deployment
   metadata:
     name: oopv-books-api
   spec:
     replicas: 2
     selector:
       matchLabels:
         app: oopv-books-api
     template:
       metadata:
         labels:
           app: oopv-books-api
       spec:
         containers:
         - name: api
           image: ghcr.io/yourusername/oopv-books-api:latest
           ports:
           - containerPort: 8080
   ```

2. **Apply Manifests**
   ```bash
   kubectl apply -f k8s/
   ```

## ?? Security Considerations

### Production Checklist
- [ ] Remove development endpoints in production
- [ ] Configure HTTPS with valid certificates
- [ ] Set up CORS policies appropriately
- [ ] Implement authentication and authorization
- [ ] Configure rate limiting
- [ ] Set up monitoring and logging
- [ ] Regular security scans of container images
- [ ] Use non-root users in containers
- [ ] Implement health checks and readiness probes

## ?? Monitoring and Logging

### Health Checks
Both services expose health check endpoints:
- API: `http://localhost:5001/health`
- Web: `http://localhost:5003/health`

### Docker Health Checks
The docker-compose.yml includes health check configurations:
```yaml
healthcheck:
  test: ["CMD", "curl", "-f", "http://localhost:8080/health"]
  interval: 30s
  timeout: 10s
  retries: 3
  start_period: 40s
```

### Logging
- Structured logging with Serilog (can be added)
- Application Insights integration for Azure
- CloudWatch integration for AWS
- Centralized logging with ELK stack

## ?? Troubleshooting

### Common Issues

1. **Port Conflicts**
   ```bash
   # Check what's using the ports
   netstat -tulpn | grep :5001
   netstat -tulpn | grep :5003
   ```

2. **Container Won't Start**
   ```bash
   # Check container logs
   docker-compose logs api
   docker-compose logs web
   ```

3. **API Connection Issues**
   ```bash
   # Test API directly
   curl http://localhost:5001/api/books
   curl http://localhost:5001/health
   ```

4. **DNS Resolution in Containers**
   ```bash
   # Check network connectivity
   docker-compose exec web ping api
   ```

### Debug Mode
To run in debug mode with more verbose logging:
```bash
docker-compose -f docker-compose.yml -f docker-compose.debug.yml up
```

## ?? Scaling

### Horizontal Scaling
- Scale API service: `docker-compose up -d --scale api=3`
- Use load balancer (nginx, HAProxy, cloud LB)
- Implement stateless design

### Vertical Scaling
- Adjust resource limits in docker-compose.yml
- Monitor memory and CPU usage
- Consider container orchestration platforms

## ?? CI/CD Integration

The project includes GitHub Actions workflow:
- Automated testing
- Security scanning
- Container image building
- Deployment to staging/production

### Webhook Deployment
Set up webhooks for automatic deployment:
1. Container registry webhooks
2. GitHub deployment environments
3. Azure DevOps pipelines
4. Jenkins integration

---

For additional support, please refer to the main [README.md](README.md) or open an issue in the repository.