using Microsoft.EntityFrameworkCore;
using ToDoApp.Models;

namespace ToDoApp.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<ToDoItem> ToDoItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    // Optional: Hier könnten Sie Model-Konfigurationen vornehmen (z.B. Schlüssel, Beziehungen, etc.)
    // Wird in den meisten einfachen Fällen nicht benötigt, da EF Core Konventionen verwendet.
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //     // Beispiel: modelBuilder.Entity<ToDoItem>().HasKey(t => t.Id);
    // }
}