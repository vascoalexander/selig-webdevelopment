using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace DbToApp.Models.Database;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abteilung> Abteilungs { get; set; }

    public virtual DbSet<Arbeit> Arbeits { get; set; }

    public virtual DbSet<Gehalt> Gehalts { get; set; }

    public virtual DbSet<Kunde> Kundes { get; set; }

    public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; }

    public virtual DbSet<Projekt> Projekts { get; set; }

    public virtual DbSet<Umsatz> Umsatzs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=ProjektDB;uid=semus;pwd=semuspw", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.11-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Abteilung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Abteilung");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Bezeichnung)
                .HasMaxLength(30)
                .HasColumnName("bezeichnung");
            entity.Property(e => e.Stadt)
                .HasMaxLength(25)
                .HasColumnName("stadt");
        });

        modelBuilder.Entity<Arbeit>(entity =>
        {
            entity.HasKey(e => new { e.MitId, e.ProId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Arbeit");

            entity.HasIndex(e => e.ProId, "pro_id");

            entity.Property(e => e.MitId)
                .HasColumnType("int(11)")
                .HasColumnName("mit_id");
            entity.Property(e => e.ProId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("pro_id");
            entity.Property(e => e.Aufgabe)
                .HasMaxLength(30)
                .HasColumnName("aufgabe");
            entity.Property(e => e.EinstDat).HasColumnName("einst_dat");

            entity.HasOne(d => d.Mit).WithMany(p => p.Arbeits)
                .HasForeignKey(d => d.MitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Arbeit_ibfk_1");

            entity.HasOne(d => d.Pro).WithMany(p => p.Arbeits)
                .HasForeignKey(d => d.ProId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Arbeit_ibfk_2");
        });

        modelBuilder.Entity<Gehalt>(entity =>
        {
            entity.HasKey(e => e.MitId).HasName("PRIMARY");

            entity.ToTable("Gehalt");

            entity.Property(e => e.MitId)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("mit_id");
            entity.Property(e => e.Gehalt1)
                .HasPrecision(15, 2)
                .HasColumnName("gehalt");

            entity.HasOne(d => d.Mit).WithOne(p => p.Gehalt)
                .HasForeignKey<Gehalt>(d => d.MitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Gehalt_ibfk_1");
        });

        modelBuilder.Entity<Kunde>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Kunde");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Firma)
                .HasMaxLength(30)
                .HasColumnName("firma");
            entity.Property(e => e.Stadt)
                .HasMaxLength(25)
                .HasColumnName("stadt");
        });

        modelBuilder.Entity<Mitarbeiter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Mitarbeiter");

            entity.HasIndex(e => e.AbtId, "abt_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.AbtId)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("abt_id");
            entity.Property(e => e.Nachname)
                .HasMaxLength(30)
                .HasColumnName("nachname");
            entity.Property(e => e.Stadt)
                .HasMaxLength(25)
                .HasColumnName("stadt");
            entity.Property(e => e.Vorname)
                .HasMaxLength(30)
                .HasColumnName("vorname");

            entity.HasOne(d => d.Abt).WithMany(p => p.Mitarbeiters)
                .HasForeignKey(d => d.AbtId)
                .HasConstraintName("Mitarbeiter_ibfk_1");
        });

        modelBuilder.Entity<Projekt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Projekt");

            entity.HasIndex(e => e.KdId, "kd_id");

            entity.Property(e => e.Id)
                .HasMaxLength(2)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Bezeichnung)
                .HasMaxLength(30)
                .HasColumnName("bezeichnung");
            entity.Property(e => e.KdId)
                .HasColumnType("int(11)")
                .HasColumnName("kd_id");
            entity.Property(e => e.Mittel)
                .HasPrecision(15, 2)
                .HasColumnName("mittel");

            entity.HasOne(d => d.Kd).WithMany(p => p.Projekts)
                .HasForeignKey(d => d.KdId)
                .HasConstraintName("Projekt_ibfk_1");
        });

        modelBuilder.Entity<Umsatz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Umsatz");

            entity.HasIndex(e => e.MitId, "mit_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.MitId)
                .HasColumnType("int(11)")
                .HasColumnName("mit_id");
            entity.Property(e => e.Umsatz1)
                .HasPrecision(15, 2)
                .HasColumnName("umsatz");

            entity.HasOne(d => d.Mit).WithMany(p => p.Umsatzs)
                .HasForeignKey(d => d.MitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Umsatz_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
