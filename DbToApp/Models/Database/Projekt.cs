using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Projekt
{
    public string Id { get; set; } = null!;

    public string Bezeichnung { get; set; } = null!;

    public decimal? Mittel { get; set; }

    public int? KdId { get; set; }

    public virtual ICollection<Arbeit> Arbeits { get; set; } = new List<Arbeit>();

    public virtual Kunde? Kd { get; set; }
}
