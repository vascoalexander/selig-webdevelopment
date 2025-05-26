using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models;

public class Article
{
    // database properties
    public int Id { get; set; }
    [MaxLength(100)]
    public required string Headline { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AuthorId { get; set; }
    public Author? Author { get; set; }

    // view properties
    public string ContentPreview => Content.Length < 100 ? Content : Content.Substring(0, 100) + "...";
}