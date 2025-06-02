using Microsoft.EntityFrameworkCore;
using Patienten.Models;

namespace Patienten.Data;

public class AppDbContext : DbContext
{
    public DbSet<Arzt> Aerzte { get; set; }
    public DbSet<Krankenkasse> Krankenkassen { get; set; }
    public DbSet<Termin> Termine { get; set; }
    public DbSet<Patient> Patienten { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>()
            .HasOne(p => p.Krankenkasse)
            .WithMany(k => k.Patienten)
            .HasForeignKey(p => p.KrankenkasseId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Termin>()
            .HasOne(t => t.Arzt)
            .WithMany(a => a.Termine)
            .HasForeignKey(a => a.ArztId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Termin>()
            .HasOne(t => t.Patient)
            .WithMany(p => p.Termine)
            .HasForeignKey(t => t.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}