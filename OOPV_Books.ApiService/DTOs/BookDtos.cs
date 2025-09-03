namespace OOPV_Books.ApiService.DTOs;

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
}

public class UpdateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
}