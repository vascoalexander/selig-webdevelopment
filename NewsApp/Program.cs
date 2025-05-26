using NewsApp.Data;
using NewsApp.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<NewsApp.Data.AppDbContext>();
builder.Services.AddScoped<NewsApp.Repositories.AppRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 35)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    );
});

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Fehler: Connection string 'MySqlAivenConnection' nicht gefunden.");
}
else
{
    // *** Verbindungstest beim Start ***
    try
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Datenbankverbindung erfolgreich hergestellt!");
            Console.WriteLine("--------------------------------------------------");
            connection.Close(); // Verbindung sofort wieder schließen
        }
    }
    catch (MySqlException ex)
    {
        Console.WriteLine("--------------------------------------------------");
        Console.Error.WriteLine($"Fehler beim Herstellen der Datenbankverbindung: {ex.Message}");
        Console.Error.WriteLine($"Connection String verwendet: {connectionString}");
        Console.Error.WriteLine($"Stack Trace: {ex.StackTrace}");
        Console.WriteLine("--------------------------------------------------");
        // Optional: Die Anwendung beenden, wenn die DB-Verbindung kritisch ist
        // Environment.Exit(1);
    }
    // **********************************
}

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

EnsureDatabase.ExecuteMigrations(app);
EnsureDatabase.SeedDatabase(app);

app.Run();