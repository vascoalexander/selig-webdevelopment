using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Data;

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

        if (!context.Authors.Any())
        {
            context.Authors.AddRange(
                new Author()
                {
                    FirstName = "Harry",
                    LastName = "Hirsch"
                },
                new Author()
                {
                    FirstName = "Karla",
                    LastName = "Kolumna"
                });
            context.SaveChanges();
        }

        if (!context.Articles.Any())
        {
            context.Articles.AddRange(
                new Article()
                {
                    Headline = "A headline",
                    Content = "blablalalsjdkasdfall",
                    CreatedAt = new DateTime(2025,1,1),
                    AuthorId = context.Authors.FirstOrDefault(a => a.FirstName == "Harry")!.Id
                },
                new Article()
                {
                    Headline = "Another headline",
                    Content = "",
                    CreatedAt = new DateTime(2024,12,1),
                    AuthorId = context.Authors.FirstOrDefault(a => a.FirstName == "Karla")!.Id
                }
            );
            context.SaveChanges();
        }
    }
}