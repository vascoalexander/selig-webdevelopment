using System.ComponentModel.DataAnnotations;

namespace TestProject.Models;

public class Position
{
    public int ID { get; set; }
    [Required(ErrorMessage = "Bitte geben sie den Artikel-Namen an")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Bitte geben sie die Anzahl an.")]
    [Range(1, 50, ErrorMessage = "Bitte im Bereich 1-50")]
    public int Anzahl { get; set; }
    [Required(ErrorMessage = "Bitte geben Sie das Geschäft an.")]
    public string? Geschaeft { get; set; }
}