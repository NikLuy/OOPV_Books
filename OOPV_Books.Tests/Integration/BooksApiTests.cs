using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using OOPV_Books.ApiService.Models;
using OOPV_Books.ApiService.DTOs;
using Xunit;

namespace OOPV_Books.Tests.Integration;

public class BooksApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public BooksApiTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [Fact]
    public async Task GetBooks_ShouldReturnSuccessAndBooks()
    {
        // Act
        var response = await _client.GetAsync("/api/books");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var books = JsonSerializer.Deserialize<List<Book>>(content, _jsonOptions);

        Assert.NotNull(books);
        Assert.True(books.Count > 0);
    }

    [Fact]
    public async Task CreateBook_ShouldReturnCreatedBook()
    {
        // Arrange
        var createBookDto = new CreateBookDto
        {
            Title = "Integration Test Book",
            Author = "Test Author",
            ISBN = "123-456-789-INT",
            PublishedDate = DateTime.Now.Date,
            Description = "A book created during integration testing",
            Genre = "Technology"
        };

        var json = JsonSerializer.Serialize(createBookDto, _jsonOptions);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync("/api/books", content);

        // Assert
        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStringAsync();
        var createdBook = JsonSerializer.Deserialize<Book>(responseContent, _jsonOptions);

        Assert.NotNull(createdBook);
        Assert.Equal(createBookDto.Title, createdBook.Title);
        Assert.Equal(createBookDto.Author, createdBook.Author);
        Assert.True(createdBook.Id > 0);
    }

    [Fact]
    public async Task GetBookById_WithValidId_ShouldReturnBook()
    {
        // Act
        var response = await _client.GetAsync("/api/books/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var book = JsonSerializer.Deserialize<Book>(content, _jsonOptions);

        Assert.NotNull(book);
        Assert.Equal(1, book.Id);
    }

    [Fact]
    public async Task GetBookById_WithInvalidId_ShouldReturnNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/books/999");

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task HealthCheck_ShouldReturnHealthy()
    {
        // Act - Using the Aspire default health check endpoint
        var response = await _client.GetAsync("/health");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        // Aspire health checks return JSON, not plain text
        Assert.NotNull(content);
        Assert.True(content.Contains("Healthy") || content.Contains("\"status\":\"Healthy\""));
    }

    [Fact]
    public async Task GetWeatherForecast_ShouldReturnForecast()
    {
        // Act
        var response = await _client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.NotNull(content);
        Assert.True(content.Length > 0);
    }
}