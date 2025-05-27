using System.ComponentModel.DataAnnotations;

namespace ProjektKunde.Models;

public class Customer
{
    public int Id { get; set; }
    [StringLength(40, ErrorMessage = "Der Name darf maximal 40 Zeichen lang sein.")]
    public required string Company { get; set; }
    public Address? Address { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
}