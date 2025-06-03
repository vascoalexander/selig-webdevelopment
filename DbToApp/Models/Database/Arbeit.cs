using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Arbeit
{
    public int MitId { get; set; }

    public string ProId { get; set; } = null!;

    public string? Aufgabe { get; set; }

    public DateOnly? EinstDat { get; set; }

    public virtual Mitarbeiter Mit { get; set; } = null!;

    public virtual Projekt Pro { get; set; } = null!;
}
