using System.ComponentModel.DataAnnotations;

namespace Patienten.Models;

public class Krankenkasse
{
    public int Id { get; set; }
    [StringLength(40, ErrorMessage = "Der Name darf maximal 20 Zeichen lang sein.")]
    public required string Name { get; set; }
    public ICollection<Patient>? Patienten { get; set; }
}