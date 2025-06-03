using System;
using System.Collections.Generic;

namespace DbToApp.Models.Database;

public partial class Kunde
{
    public int Id { get; set; }

    public string Firma { get; set; } = null!;

    public string Stadt { get; set; } = null!;

    public virtual ICollection<Projekt> Projekts { get; set; } = new List<Projekt>();
}
