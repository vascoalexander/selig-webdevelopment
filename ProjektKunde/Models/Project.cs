using System.ComponentModel.DataAnnotations;

namespace ProjektKunde.Models;

public class Project
{
    public int Id { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    [StringLength(40, ErrorMessage = "Der Titel darf maximal 40 Zeichen lang sein.")]
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}