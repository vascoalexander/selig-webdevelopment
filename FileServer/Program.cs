using FileServer.Models;
using FileServer.Repositories;
using MySqlConnector;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MariaDbServerVersion(new Version(10, 11, 11)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    );
});

if (string.IsNullOrEmpty(connectionString))
{
    Console.WriteLine("Fehler: Connection string nicht gefunden.");
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

builder.Services
    .AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
builder.Services
    .AddScoped<IUserFileRepository, UserFileRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

EnsureDatabase.ExecuteMigrations(app);
await EnsureIdentity.SeedDefaultAccounts(app);

app.Run();