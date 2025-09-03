namespace OOPV_Books.ApiService.Models;

public class Review
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 stars
    public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
    public Book? Book { get; set; }
}