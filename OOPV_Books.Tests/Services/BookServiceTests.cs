using OOPV_Books.ApiService.Services;
using OOPV_Books.ApiService.Models;
using Xunit;

namespace OOPV_Books.Tests.Services;

public class BookServiceTests
{
    [Fact]
    public async Task GetAllBooksAsync_ShouldReturnBooks()
    {
        // Arrange
        var bookService = new InMemoryBookService();

        // Act
        var books = await bookService.GetAllBooksAsync();

        // Assert
        Assert.NotNull(books);
        Assert.True(books.Any());
    }

    [Fact]
    public async Task CreateBookAsync_ShouldAddNewBook()
    {
        // Arrange
        var bookService = new InMemoryBookService();
        var newBook = new Book
        {
            Title = "Test Book",
            Author = "Test Author",
            ISBN = "123-456-789",
            PublishedDate = DateTime.Now,
            Description = "Test Description",
            Genre = "Test Genre"
        };

        // Act
        var createdBook = await bookService.CreateBookAsync(newBook);

        // Assert
        Assert.NotNull(createdBook);
        Assert.True(createdBook.Id > 0);
        Assert.Equal("Test Book", createdBook.Title);
        Assert.Equal("Test Author", createdBook.Author);
    }

    [Fact]
    public async Task GetBookByIdAsync_WithValidId_ShouldReturnBook()
    {
        // Arrange
        var bookService = new InMemoryBookService();
        
        // Act
        var book = await bookService.GetBookByIdAsync(1);

        // Assert
        Assert.NotNull(book);
        Assert.Equal(1, book.Id);
    }

    [Fact]
    public async Task GetBookByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        var bookService = new InMemoryBookService();
        
        // Act
        var book = await bookService.GetBookByIdAsync(999);

        // Assert
        Assert.Null(book);
    }

    [Fact]
    public async Task UpdateBookAsync_WithValidData_ShouldUpdateBook()
    {
        // Arrange
        var bookService = new InMemoryBookService();
        var updateBook = new Book
        {
            Title = "Updated Title",
            Author = "Updated Author",
            ISBN = "987-654-321",
            PublishedDate = DateTime.Now.AddYears(-1),
            Description = "Updated Description",
            Genre = "Updated Genre"
        };

        // Act
        var updatedBook = await bookService.UpdateBookAsync(1, updateBook);

        // Assert
        Assert.NotNull(updatedBook);
        Assert.Equal("Updated Title", updatedBook.Title);
        Assert.Equal("Updated Author", updatedBook.Author);
    }

    [Fact]
    public async Task DeleteBookAsync_WithValidId_ShouldReturnTrue()
    {
        // Arrange
        var bookService = new InMemoryBookService();

        // Act
        var result = await bookService.DeleteBookAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteBookAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        var bookService = new InMemoryBookService();

        // Act
        var result = await bookService.DeleteBookAsync(999);

        // Assert
        Assert.False(result);
    }
}