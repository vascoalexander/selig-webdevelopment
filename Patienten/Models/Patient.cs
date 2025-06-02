using System.ComponentModel.DataAnnotations;

namespace Patienten.Models;

public class Patient
{
    public int Id { get; set; }
    [StringLength(20, ErrorMessage = "Der Nachname darf maximal 20 Zeichen lang sein.")]
    public required string Nachname { get; set; }
    [StringLength(20, ErrorMessage = "Der Vorname darf maximal 20 Zeichen lang sein.")]
    public required string Vorname { get; set; }
    public required DateTime GebDat { get; set; }
    public int KrankenkasseId { get; set; }
    public Krankenkasse? Krankenkasse { get; set; }
    public ICollection<Termin>? Termine { get; set; }

    public string Fullname => Vorname + " " + Nachname;
}