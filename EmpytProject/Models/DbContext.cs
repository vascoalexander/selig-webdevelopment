// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace EmpytProject.Models
{
    public class EmptyProjectDbContext : DbContext
    {
        public EmptyProjectDbContext(DbContextOptions<EmptyProjectDbContext> options)
            : base(options)
        {
        }

        // Definieren Sie DbSets für Ihre Entitäten
        public DbSet<Employee> Products { get; set; }

        // Optional: Konfigurieren Sie das Modell, z.B. für Tabellennamen oder Spalteneigenschaften
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Beispiel: Eine Eigenschaft als eindeutig markieren
            // modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();

            // Beispiel: Standard-Tabellennamen (Pluralisierung) überschreiben
            // modelBuilder.Entity<Product>().ToTable("MyProductsTable");

            base.OnModelCreating(modelBuilder);
        }
    }
}