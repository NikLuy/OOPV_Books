using OOPV_Books.ApiService.Models;

namespace OOPV_Books.ApiService.Services;

public interface IBookService
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> CreateBookAsync(Book book);
    Task<Book?> UpdateBookAsync(int id, Book book);
    Task<bool> DeleteBookAsync(int id);
}

public class InMemoryBookService : IBookService
{
    private readonly List<Book> _books = new();
    private int _nextId = 1;

    public InMemoryBookService()
    {
        // Seed some sample data
        SeedData();
    }

    private void SeedData()
    {
        _books.AddRange(new[]
        {
            new Book
            {
                Id = _nextId++,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                ISBN = "978-0132350884",
                PublishedDate = new DateTime(2008, 8, 1),
                Description = "A Handbook of Agile Software Craftsmanship",
                Genre = "Technology",
                Reviews = new List<Review>()
            },
            new Book
            {
                Id = _nextId++,
                Title = "The Pragmatic Programmer",
                Author = "Andrew Hunt, David Thomas",
                ISBN = "978-0135957059",
                PublishedDate = new DateTime(2019, 9, 13),
                Description = "Your journey to mastery",
                Genre = "Technology",
                Reviews = new List<Review>()
            }
        });
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return Task.FromResult(_books.AsEnumerable());
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        return Task.FromResult(book);
    }

    public Task<Book> CreateBookAsync(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return Task.FromResult(book);
    }

    public Task<Book?> UpdateBookAsync(int id, Book book)
    {
        var existingBook = _books.FirstOrDefault(b => b.Id == id);
        if (existingBook == null)
            return Task.FromResult<Book?>(null);

        existingBook.Title = book.Title;
        existingBook.Author = book.Author;
        existingBook.ISBN = book.ISBN;
        existingBook.PublishedDate = book.PublishedDate;
        existingBook.Description = book.Description;
        existingBook.Genre = book.Genre;

        return Task.FromResult<Book?>(existingBook);
    }

    public Task<bool> DeleteBookAsync(int id)
    {
        var book = _books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return Task.FromResult(false);

        _books.Remove(book);
        return Task.FromResult(true);
    }
}