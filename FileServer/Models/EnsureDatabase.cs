using Microsoft.EntityFrameworkCore;

namespace FileServer.Models;

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
}