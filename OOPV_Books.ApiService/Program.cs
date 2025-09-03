using OOPV_Books.ApiService.Models;
using OOPV_Books.ApiService.DTOs;
using OOPV_Books.ApiService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Note: Health checks are already provided by AddServiceDefaults()

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Register our services
builder.Services.AddSingleton<IBookService, InMemoryBookService>();
builder.Services.AddSingleton<IReviewService, InMemoryReviewService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseCors("AllowAll");

// Note: Health check endpoints are already mapped by MapDefaultEndpoints()

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Book endpoints
app.MapGet("/api/books", async (IBookService bookService) =>
{
    var books = await bookService.GetAllBooksAsync();
    return Results.Ok(books);
})
.WithName("GetBooks")
.WithTags("Books");

app.MapGet("/api/books/{id}", async (int id, IBookService bookService) =>
{
    var book = await bookService.GetBookByIdAsync(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();
})
.WithName("GetBookById")
.WithTags("Books");

app.MapPost("/api/books", async (CreateBookDto dto, IBookService bookService) =>
{
    var book = new Book
    {
        Title = dto.Title,
        Author = dto.Author,
        ISBN = dto.ISBN,
        PublishedDate = dto.PublishedDate,
        Description = dto.Description,
        Genre = dto.Genre
    };
    
    var createdBook = await bookService.CreateBookAsync(book);
    return Results.Created($"/api/books/{createdBook.Id}", createdBook);
})
.WithName("CreateBook")
.WithTags("Books");

app.MapPut("/api/books/{id}", async (int id, UpdateBookDto dto, IBookService bookService) =>
{
    var book = new Book
    {
        Title = dto.Title,
        Author = dto.Author,
        ISBN = dto.ISBN,
        PublishedDate = dto.PublishedDate,
        Description = dto.Description,
        Genre = dto.Genre
    };
    
    var updatedBook = await bookService.UpdateBookAsync(id, book);
    return updatedBook is not null ? Results.Ok(updatedBook) : Results.NotFound();
})
.WithName("UpdateBook")
.WithTags("Books");

app.MapDelete("/api/books/{id}", async (int id, IBookService bookService) =>
{
    var success = await bookService.DeleteBookAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteBook")
.WithTags("Books");

// Review endpoints
app.MapGet("/api/reviews", async (IReviewService reviewService) =>
{
    var reviews = await reviewService.GetAllReviewsAsync();
    return Results.Ok(reviews);
})
.WithName("GetReviews")
.WithTags("Reviews");

app.MapGet("/api/books/{bookId}/reviews", async (int bookId, IReviewService reviewService) =>
{
    var reviews = await reviewService.GetReviewsByBookIdAsync(bookId);
    return Results.Ok(reviews);
})
.WithName("GetReviewsByBookId")
.WithTags("Reviews");

app.MapGet("/api/reviews/{id}", async (int id, IReviewService reviewService) =>
{
    var review = await reviewService.GetReviewByIdAsync(id);
    return review is not null ? Results.Ok(review) : Results.NotFound();
})
.WithName("GetReviewById")
.WithTags("Reviews");

app.MapPost("/api/reviews", async (CreateReviewDto dto, IReviewService reviewService, IBookService bookService) =>
{
    // Verify book exists
    var book = await bookService.GetBookByIdAsync(dto.BookId);
    if (book is null)
        return Results.BadRequest("Book not found");

    var review = new Review
    {
        BookId = dto.BookId,
        ReviewerName = dto.ReviewerName,
        ReviewText = dto.ReviewText,
        Rating = dto.Rating
    };
    
    var createdReview = await reviewService.CreateReviewAsync(review);
    return Results.Created($"/api/reviews/{createdReview.Id}", createdReview);
})
.WithName("CreateReview")
.WithTags("Reviews");

app.MapPut("/api/reviews/{id}", async (int id, UpdateReviewDto dto, IReviewService reviewService) =>
{
    var review = new Review
    {
        ReviewerName = dto.ReviewerName,
        ReviewText = dto.ReviewText,
        Rating = dto.Rating
    };
    
    var updatedReview = await reviewService.UpdateReviewAsync(id, review);
    return updatedReview is not null ? Results.Ok(updatedReview) : Results.NotFound();
})
.WithName("UpdateReview")
.WithTags("Reviews");

app.MapDelete("/api/reviews/{id}", async (int id, IReviewService reviewService) =>
{
    var success = await reviewService.DeleteReviewAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteReview")
.WithTags("Reviews");

// Keep the original weather forecast endpoint for testing
string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Make Program class public for testing
public partial class Program { }
