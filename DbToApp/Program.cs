using DbToApp.Models.Database;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();