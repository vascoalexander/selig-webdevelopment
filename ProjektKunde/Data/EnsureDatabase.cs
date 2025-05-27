using ProjektKunde.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjektKunde.Data;

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

        if (!context.Customers.Any())
        {
            var customer1 = new Customer
            {
                Company = "BITLC GmbH",
                Address = new Address
                {
                    Country = "Germany",
                    State = "NRW",
                    City = "Dortmund",
                    ZipCode = "44137"
                }
            };

            var customer2 = new Customer
            {
                Company = "Materna SE",
                Address = new Address
                {
                    Country = "Germany",
                    State = "NRW",
                    City = "Dortmund",
                    ZipCode = "44137"
                }
            };
            context.Customers.AddRange(customer1, customer2);
            context.SaveChanges();
        }

        if (!context.Projects.Any())
        {
            var bitlc = context.Customers.FirstOrDefault(c => c.Company == "BITLC GmbH");
            var materna = context.Customers.FirstOrDefault(c => c.Company == "Materna SE");

            context.Projects.AddRange(
                new Project
                {
                    Title = "Website Relaunch",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus lorem leo, sagittis quis massa et, tincidunt vehicula nunc. Donec id ante ut elit interdum viverra a a urna.",
                    Start = new DateTime(2024,03,01),
                    End = new DateTime(2024,07,23),
                    CustomerId = bitlc!.Id,
                    Customer = bitlc
                },
                new Project
                {
                    Title = "App Entwicklung",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus lorem leo, sagittis quis massa et, tincidunt vehicula nunc. Donec id ante ut elit interdum viverra a a urna.",
                    Start = new DateTime(2024,06,08),
                    End = new DateTime(2024,11,28),
                    CustomerId = bitlc!.Id,
                    Customer = bitlc
                },
                new Project
                {
                    Title = "Marketing Kampagne",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus lorem leo, sagittis quis massa et, tincidunt vehicula nunc. Donec id ante ut elit interdum viverra a a urna.",
                    Start = new DateTime(2024,12,31),
                    End = new DateTime(2025,02,12),
                    CustomerId = materna.Id,
                    Customer = materna
                },
                new Project
                {
                    Title = "Datenbank Optimierung",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus lorem leo, sagittis quis massa et, tincidunt vehicula nunc. Donec id ante ut elit interdum viverra a a urna.",
                    Start = new DateTime(2025,01,01),
                    End = new DateTime(2025,12,24),
                    CustomerId = materna.Id,
                    Customer = materna
                }
            );
            context.SaveChanges();
        }
    }
}