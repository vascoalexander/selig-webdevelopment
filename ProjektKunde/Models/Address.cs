using System.ComponentModel.DataAnnotations;

namespace ProjektKunde.Models;

public class Address
{
    public int Id { get; set; }
    [StringLength(20, ErrorMessage = "Das Land darf maximal 20 Zeichen lang sein.")]
    public required string Country { get; set; }
    [StringLength(20, ErrorMessage = "Das Bundesland darf maximal 20 Zeichen lang sein.")]
    public required string State { get; set; }
    [StringLength(40, ErrorMessage = "Die Stadt darf maximal 40 Zeichen lang sein.")]
    public required string City { get; set; }
    [StringLength(20, ErrorMessage = "Die PLZ darf maximal 20 Zeichen lang sein.")]
    public required string ZipCode { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
}