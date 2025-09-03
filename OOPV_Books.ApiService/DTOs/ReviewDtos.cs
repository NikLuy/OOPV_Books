namespace OOPV_Books.ApiService.DTOs;

public class CreateReviewDto
{
    public int BookId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 stars
}

public class UpdateReviewDto
{
    public string ReviewerName { get; set; } = string.Empty;
    public string ReviewText { get; set; } = string.Empty;
    public int Rating { get; set; } // 1-5 stars
}