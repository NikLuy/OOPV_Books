using OOPV_Books.ApiService.Models;
using OOPV_Books.ApiService.DTOs;
using System.Text.Json;

namespace OOPV_Books.Web.Services;

public class BooksApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public BooksApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    // Books API
    public async Task<List<Book>> GetBooksAsync()
    {
        var response = await _httpClient.GetAsync("/api/books");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Book>>(json, _jsonOptions) ?? new List<Book>();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/books/{id}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Book>(json, _jsonOptions);
    }

    public async Task<Book> CreateBookAsync(CreateBookDto book)
    {
        var json = JsonSerializer.Serialize(book, _jsonOptions);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/books", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Book>(responseJson, _jsonOptions)!;
    }

    public async Task<Book> UpdateBookAsync(int id, UpdateBookDto book)
    {
        var json = JsonSerializer.Serialize(book, _jsonOptions);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"/api/books/{id}", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Book>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteBookAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/books/{id}");
        response.EnsureSuccessStatusCode();
    }

    // Reviews API
    public async Task<List<Review>> GetReviewsByBookIdAsync(int bookId)
    {
        var response = await _httpClient.GetAsync($"/api/books/{bookId}/reviews");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<List<Review>>(json, _jsonOptions) ?? new List<Review>();
    }

    public async Task<Review> CreateReviewAsync(CreateReviewDto review)
    {
        var json = JsonSerializer.Serialize(review, _jsonOptions);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("/api/reviews", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Review>(responseJson, _jsonOptions)!;
    }

    public async Task<Review> UpdateReviewAsync(int id, UpdateReviewDto review)
    {
        var json = JsonSerializer.Serialize(review, _jsonOptions);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"/api/reviews/{id}", content);
        response.EnsureSuccessStatusCode();

        var responseJson = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Review>(responseJson, _jsonOptions)!;
    }

    public async Task DeleteReviewAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/reviews/{id}");
        response.EnsureSuccessStatusCode();
    }
}