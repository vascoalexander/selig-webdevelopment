using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Umsatz
{
    public int Id { get; set; }

    public int MitId { get; set; }

    public DateOnly Datum { get; set; }

    public decimal Umsatz1 { get; set; }

    public virtual Mitarbeiter Mit { get; set; } = null!;
}
