using OOPV_Books.ApiService.Models;

namespace OOPV_Books.ApiService.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetAllReviewsAsync();
    Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId);
    Task<Review?> GetReviewByIdAsync(int id);
    Task<Review> CreateReviewAsync(Review review);
    Task<Review?> UpdateReviewAsync(int id, Review review);
    Task<bool> DeleteReviewAsync(int id);
}

public class InMemoryReviewService : IReviewService
{
    private readonly List<Review> _reviews = new();
    private readonly IBookService _bookService;
    private int _nextId = 1;

    public InMemoryReviewService(IBookService bookService)
    {
        _bookService = bookService;
        SeedData();
    }

    private void SeedData()
    {
        _reviews.AddRange(new[]
        {
            new Review
            {
                Id = _nextId++,
                BookId = 1,
                ReviewerName = "John Doe",
                ReviewText = "Excellent book for software developers. Highly recommended!",
                Rating = 5,
                ReviewDate = DateTime.UtcNow.AddDays(-10)
            },
            new Review
            {
                Id = _nextId++,
                BookId = 1,
                ReviewerName = "Jane Smith",
                ReviewText = "Great insights into writing maintainable code.",
                Rating = 4,
                ReviewDate = DateTime.UtcNow.AddDays(-5)
            }
        });
    }

    public Task<IEnumerable<Review>> GetAllReviewsAsync()
    {
        return Task.FromResult(_reviews.AsEnumerable());
    }

    public Task<IEnumerable<Review>> GetReviewsByBookIdAsync(int bookId)
    {
        var reviews = _reviews.Where(r => r.BookId == bookId);
        return Task.FromResult(reviews);
    }

    public Task<Review?> GetReviewByIdAsync(int id)
    {
        var review = _reviews.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(review);
    }

    public Task<Review> CreateReviewAsync(Review review)
    {
        review.Id = _nextId++;
        review.ReviewDate = DateTime.UtcNow;
        _reviews.Add(review);
        return Task.FromResult(review);
    }

    public Task<Review?> UpdateReviewAsync(int id, Review review)
    {
        var existingReview = _reviews.FirstOrDefault(r => r.Id == id);
        if (existingReview == null)
            return Task.FromResult<Review?>(null);

        existingReview.ReviewerName = review.ReviewerName;
        existingReview.ReviewText = review.ReviewText;
        existingReview.Rating = review.Rating;

        return Task.FromResult<Review?>(existingReview);
    }

    public Task<bool> DeleteReviewAsync(int id)
    {
        var review = _reviews.FirstOrDefault(r => r.Id == id);
        if (review == null)
            return Task.FromResult(false);

        _reviews.Remove(review);
        return Task.FromResult(true);
    }
}