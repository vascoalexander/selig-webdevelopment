using Patienten.Models;
using Microsoft.EntityFrameworkCore;

namespace Patienten.Data;

public class EnsureDatabase
{
    public static void ExecuteMigrations(IApplicationBuilder app)
    {
        var context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<AppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    public static void SeedDatabase(IApplicationBuilder app)
    {
        var context = app
            .ApplicationServices
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<AppDbContext>();

        if (!context.Krankenkassen.Any())
        {
            var krankenkasse1 = new Krankenkasse()
            {
                Name = "Barmer"
            };
            var krankenkasse2 = new Krankenkasse()
            {
                Name = "Techniker Krankenkasse"
            };

            context.Krankenkassen.AddRange(krankenkasse1, krankenkasse2);
            context.SaveChanges();
        }

        if (!context.Aerzte.Any())
        {
            var arzt1 = new Arzt()
            {
                Nachname = "Leberowski",
                Vorname = "Adam"
            };
            var arzt2 = new Arzt()
            {
                Nachname = "di Caprio",
                Vorname = "Rüdiger"
            };

            context.Aerzte.AddRange(arzt1, arzt2);
            context.SaveChanges();
        }

        if (!context.Patienten.Any())
        {
            var barmer = context.Krankenkassen.FirstOrDefault(k => k.Name == "Barmer");
            var tk = context.Krankenkassen.FirstOrDefault(k => k.Name == "Techniker Krankenkasse");

            var patient1 = new Patient()
            {
                Vorname = "Hans",
                Nachname = "Müller",
                GebDat = new DateTime(1967,7,21),
                KrankenkasseId = barmer.Id
            };
            var patient2 = new Patient()
            {
                Vorname = "Theo",
                Nachname = "von Holthausen",
                GebDat = new DateTime(1934, 10, 2),
                KrankenkasseId = tk.Id
            };

            context.Patienten.AddRange(patient1, patient2);
            context.SaveChanges();
        }

        if (!context.Termine.Any())
        {
            var arztAdam = context.Aerzte.FirstOrDefault(a => a.Vorname == "Adam");
            var arztDiCaprio = context.Aerzte.FirstOrDefault(a => a.Vorname == "Rüdiger");
            var patHans = context.Patienten.FirstOrDefault(p => p.Vorname == "Hans");
            var patTheo = context.Patienten.FirstOrDefault(p => p.Vorname == "Theo");

            var termin1 = new Termin()
            {
                TerminZeit = new DateTime(2025,6,2),
                Wahrgenommen = false,
                PatientId = patHans.Id,
                ArztId = arztAdam.Id,

            };
            var termin2 = new Termin()
            {
                TerminZeit = new DateTime(2025, 6,4),
                Wahrgenommen = false,
                PatientId = patTheo.Id,
                ArztId = arztDiCaprio.Id
            };

            context.Termine.AddRange(termin1, termin2);
            context.SaveChanges();
        }
    }
}