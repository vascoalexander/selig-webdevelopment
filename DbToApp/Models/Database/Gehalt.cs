using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Gehalt
{
    public int MitId { get; set; }

    public decimal Gehalt1 { get; set; }

    public virtual Mitarbeiter Mit { get; set; } = null!;
}
