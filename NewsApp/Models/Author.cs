using System.ComponentModel.DataAnnotations;

namespace NewsApp.Models;

public class Author
{
    // database properties
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public virtual ICollection<Article> Articles { get; set; } = [];

    // view properties

    public string Fullname => $"{FirstName} {LastName}";
}