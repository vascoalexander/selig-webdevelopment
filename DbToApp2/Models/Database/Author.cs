using System;
using System.Collections.Generic;

namespace DbToApp2.Models.Database;

public partial class Author
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public string FullName => $"{FirstName} {LastName}";
}
