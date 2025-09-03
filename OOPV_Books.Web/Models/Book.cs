namespace OOPV_Books.ApiService.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public List<Review> Reviews { get; set; } = new();
    public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
}