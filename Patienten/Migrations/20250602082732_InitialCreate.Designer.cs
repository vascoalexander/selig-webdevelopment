﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Patienten.Data;

#nullable disable

namespace Patienten.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250602082732_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Patienten.Models.Arzt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Aerzte");
                });

            modelBuilder.Entity("Patienten.Models.Krankenkasse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Krankenkassen");
                });

            modelBuilder.Entity("Patienten.Models.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("GebDat")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("KrankenkasseId")
                        .HasColumnType("int");

                    b.Property<string>("Nachname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Vorname")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.HasIndex("KrankenkasseId");

                    b.ToTable("Patienten");
                });

            modelBuilder.Entity("Patienten.Models.Termin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ArztId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TerminZeit")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Wahrgenommen")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("ArztId");

                    b.HasIndex("PatientId");

                    b.ToTable("Termine");
                });

            modelBuilder.Entity("Patienten.Models.Patient", b =>
                {
                    b.HasOne("Patienten.Models.Krankenkasse", "Krankenkasse")
                        .WithMany("Patienten")
                        .HasForeignKey("KrankenkasseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Krankenkasse");
                });

            modelBuilder.Entity("Patienten.Models.Termin", b =>
                {
                    b.HasOne("Patienten.Models.Arzt", "Arzt")
                        .WithMany("Termine")
                        .HasForeignKey("ArztId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Patienten.Models.Patient", "Patient")
                        .WithMany("Termine")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Arzt");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Patienten.Models.Arzt", b =>
                {
                    b.Navigation("Termine");
                });

            modelBuilder.Entity("Patienten.Models.Krankenkasse", b =>
                {
                    b.Navigation("Patienten");
                });

            modelBuilder.Entity("Patienten.Models.Patient", b =>
                {
                    b.Navigation("Termine");
                });
#pragma warning restore 612, 618
        }
    }
}
