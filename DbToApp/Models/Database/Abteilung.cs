using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Abteilung
{
    public string Id { get; set; } = null!;

    public string Bezeichnung { get; set; } = null!;

    public string? Stadt { get; set; }

    public virtual ICollection<Mitarbeiter> Mitarbeiters { get; set; } = new List<Mitarbeiter>();
}
