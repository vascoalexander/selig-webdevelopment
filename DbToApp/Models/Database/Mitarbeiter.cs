using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Mitarbeiter
{
    public int Id { get; set; }

    public string Nachname { get; set; } = null!;

    public string Vorname { get; set; } = null!;

    public string? AbtId { get; set; }

    public string? Stadt { get; set; }

    public virtual Abteilung? Abt { get; set; }

    public virtual ICollection<Arbeit> Arbeits { get; set; } = new List<Arbeit>();

    public virtual Gehalt? Gehalt { get; set; }

    public virtual ICollection<Umsatz> Umsatzs { get; set; } = new List<Umsatz>();
}
