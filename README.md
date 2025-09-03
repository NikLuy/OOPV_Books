# OOPV Books - Book Review Management System

A comprehensive book review management web application built with .NET 9, Blazor Server, and ASP.NET Core Web API.

## ?? Project Overview

This project demonstrates a **complete DevOps-ready web application** featuring:

- ?? **Full CRUD Operations** for books and reviews
- ?? **Star Rating System** (1-5 stars) 
- ?? **Responsive Web Interface** built with Blazor Server
- ?? **RESTful API** for backend operations
- ?? **Complete Containerization** with Docker
- ?? **CI/CD Pipeline** with automated testing and deployment
- ?? **Cloud Deployment Ready** for major platforms

## ? Features

- ?? **Book Management**: Add, edit, view, and delete books with detailed information
- ? **Review System**: Write and read book reviews with star ratings
- ?? **Browse & Search**: Easily find books and reviews
- ?? **Dashboard**: Statistics and recent books overview
- ?? **Responsive Design**: Works on desktop, tablet, and mobile devices
- ?? **Containerized**: Ready for deployment with Docker
- ?? **Modern Tech Stack**: Built with .NET 9 and Blazor Server

## ??? Technology Stack

- **Backend**: ASP.NET Core Web API (.NET 9)
- **Frontend**: Blazor Server (.NET 9)
- **Containerization**: Docker & Docker Compose
- **Architecture**: Microservices with .NET Aspire
- **UI Framework**: Bootstrap 5
- **Testing**: xUnit (Unit & Integration tests)
- **CI/CD**: GitHub Actions
- **Data Storage**: In-memory (easily extendable to Entity Framework)

## ?? Quick Start

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Git](https://git-scm.com/)

### ?? Docker Deployment (Recommended)

1. **Clone and deploy**
   ```bash
   git clone <repository-url>
   cd OOPV_Books
   docker-compose up -d --build
   ```

2. **Access the application**
   - ?? **Web Application**: http://localhost:5003
   - ?? **API Service**: http://localhost:5001
   - ?? **API Documentation**: http://localhost:5001/openapi/v1.json

3. **Stop the application**
   ```bash
   docker-compose down
   ```

### ?? Local Development

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd OOPV_Books
   ```

2. **Run with .NET CLI**
   ```bash
   # Start the API service
   cd OOPV_Books.ApiService
   dotnet run

   # In another terminal, start the web application
   cd OOPV_Books.Web
   dotnet run
   ```

3. **Access the application**
   - Web Application: `https://localhost:7042`
   - API Service: `https://localhost:7041`

### ?? Build Scripts

**Windows:**
```cmd
# Verify Docker setup and build images
verify-docker.bat

# Build and optionally start the application  
build-docker.bat
```

**Linux/macOS:**
```bash
# Make scripts executable
chmod +x verify-docker.sh build-docker.sh

# Verify Docker setup and build images
./verify-docker.sh

# Build and optionally start the application
./build-docker.sh
```

## ?? API Endpoints

### Books
- `GET /api/books` - Get all books
- `GET /api/books/{id}` - Get book by ID
- `POST /api/books` - Create a new book
- `PUT /api/books/{id}` - Update a book
- `DELETE /api/books/{id}` - Delete a book

### Reviews
- `GET /api/reviews` - Get all reviews
- `GET /api/books/{bookId}/reviews` - Get reviews for a book
- `GET /api/reviews/{id}` - Get review by ID
- `POST /api/reviews` - Create a new review
- `PUT /api/reviews/{id}` - Update a review
- `DELETE /api/reviews/{id}` - Delete a review

### Health & Monitoring
- `GET /health` - Health check endpoint
- `GET /openapi/v1.json` - OpenAPI specification

## ??? Application Architecture

```
???????????????????    HTTP/API    ???????????????????
?   Blazor Web    ??????????????????   ASP.NET API   ?
?   (Frontend)    ?                ?   (Backend)     ?
?   Port: 5003    ?                ?   Port: 5001    ?
???????????????????                ???????????????????
         ?                                   ?
         ?                                   ?
???????????????????                ???????????????????
?   Docker Web    ?                ?   Docker API    ?
?   Container     ?                ?   Container     ?
???????????????????                ???????????????????
```

## ?? Project Structure

```
OOPV_Books/
??? OOPV_Books.ApiService/          # Web API backend
?   ??? Models/                     # Data models (Book, Review)
?   ??? DTOs/                       # Data transfer objects
?   ??? Services/                   # Business logic services
?   ??? Dockerfile                  # API container definition
?   ??? Program.cs                  # API configuration
??? OOPV_Books.Web/                 # Blazor Server frontend
?   ??? Components/                 # Blazor components
?   ?   ??? Pages/                  # Page components
?   ?   ??? Layout/                 # Layout components
?   ??? Services/                   # API client services
?   ??? Dockerfile                  # Web container definition
?   ??? Program.cs                  # Web app configuration
??? OOPV_Books.Tests/               # Test project
?   ??? Services/                   # Unit tests
?   ??? Integration/                # Integration tests
??? OOPV_Books.ServiceDefaults/     # Shared Aspire configuration
??? OOPV_Books.AppHost/             # Aspire orchestration
??? docker-compose.yml              # Container orchestration
??? .github/workflows/ci-cd.yml     # CI/CD pipeline
??? README.md                       # This file
??? DEPLOYMENT.md                   # Deployment guide
??? PROJECT_SUMMARY.md              # Project completion summary
```

## ?? Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Test specific project
dotnet test OOPV_Books.Tests/
```

**Test Coverage:**
- ? Unit Tests: Service layer business logic
- ? Integration Tests: API endpoints and HTTP responses
- ? Health Check Tests: Application monitoring
- ? **All 13 tests passing** ?

## ?? Deployment Options

### ?? Cloud Platforms
- **Azure**: Container Instances, Container Apps, App Services
- **AWS**: ECS/Fargate, Elastic Beanstalk
- **Google Cloud**: Cloud Run, GKE
- **Kubernetes**: Any K8s cluster
- **Docker Swarm**: Multi-node deployment

### ?? CI/CD Pipeline
The project includes a complete GitHub Actions workflow:
- ? **Automated Testing** (Build, Unit, Integration)
- ? **Code Quality** (Linting, Formatting)
- ? **Security Scanning** (Trivy vulnerability scanner)
- ? **Container Building** (Multi-platform images)
- ? **Deployment Automation** (Staging & Production)

## ?? DevOps Features

- ?? **Multi-stage Dockerfiles** with optimized layers
- ?? **Security**: Non-root containers, vulnerability scanning
- ?? **Monitoring**: Health checks, structured logging ready
- ?? **Scalability**: Horizontal scaling support
- ??? **Production Ready**: HTTPS, CORS, error handling
- ?? **Documentation**: Comprehensive guides and API docs

## ?? Development Workflow

1. **Local Development**: Use .NET CLI for rapid development
2. **Testing**: Build and test with Docker to ensure production compatibility
3. **Deployment**: Use Docker Compose for consistent deployment across environments
4. **Monitoring**: Built-in health checks and ready for APM integration

## ?? What's Included

? **Complete Application** with real functionality  
? **Professional UI** with responsive design  
? **RESTful API** following best practices  
? **Docker Containerization** production-ready  
? **Automated CI/CD** with GitHub Actions  
? **Comprehensive Testing** (Unit + Integration)  
? **Security Scanning** and best practices  
? **Documentation** for deployment and usage  
? **Cloud Deployment Ready** for major platforms  

## ?? Future Enhancements

- [ ] User authentication and authorization (Identity/Auth0)
- [ ] Persistent database (Entity Framework with PostgreSQL/SQL Server)
- [ ] Advanced search and filtering capabilities
- [ ] Book cover image uploads and management
- [ ] User profiles and personalized reading lists
- [ ] Email notifications for new reviews
- [ ] Advanced analytics and reporting dashboard
- [ ] Integration with external book APIs (Google Books, Open Library)
- [ ] Mobile app with .NET MAUI
- [ ] Real-time notifications with SignalR

## ?? Additional Resources

- [Deployment Guide](DEPLOYMENT.md) - Detailed deployment instructions
- [Project Summary](PROJECT_SUMMARY.md) - Complete feature overview
- [API Documentation](http://localhost:5001/openapi/v1.json) - Interactive API docs
- [Docker Hub Images](https://hub.docker.com/) - Container registry

## ?? Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## ?? License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ?? Acknowledgments

- Built with [.NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- UI components from [Bootstrap 5](https://getbootstrap.com/)
- Icons from [Bootstrap Icons](https://icons.getbootstrap.com/)
- Container orchestration with [Docker](https://www.docker.com/)
- CI/CD powered by [GitHub Actions](https://github.com/features/actions)

---

**?? Ready to deploy? Check out the [Deployment Guide](DEPLOYMENT.md) for detailed instructions!**