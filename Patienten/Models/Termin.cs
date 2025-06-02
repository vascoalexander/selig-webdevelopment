namespace Patienten.Models;

public class Termin
{
    public int Id { get; set; }
    public DateTime TerminZeit { get; set; }

    public int ArztId { get; set; }
    public Arzt? Arzt { get; set; }

    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    public bool Wahrgenommen { get; set; }
}