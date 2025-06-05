using System;
using System.Collections.Generic;

namespace DbToApp2.Models.Database;

public partial class Article
{
    public int Id { get; set; }

    public string Headline { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Created { get; set; }

    public int AuthorId { get; set; }

    public string ImageFile { get; set; } = null!;

    public virtual Author? Author { get; set; } = null!;

    public string ContentPreview =>
        string.IsNullOrEmpty(Content) || Content.Length < 100
            ? Content
            : Content.Substring(0, 100);
}
