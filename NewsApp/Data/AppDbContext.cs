using NewsApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NewsApp.Data;


public class AppDbContext : DbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<Author> Authors { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    // Optional: Hier könnten Sie Model-Konfigurationen vornehmen (z.B. Schlüssel, Beziehungen, etc.)
    // Wird in den meisten einfachen Fällen nicht benötigt, da EF Core Konventionen verwendet.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>()
            .HasOne(a => a.Author)
            .WithMany(a => a.Articles)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}