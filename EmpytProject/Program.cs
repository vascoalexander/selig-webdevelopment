using Microsoft.EntityFrameworkCore;
using EmpytProject.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
// builder.Services.AddDbContext<EmpytProjectDBContext>(opts =>
//         opts.UseMySql(builder.Configuration.GetConnectionString("db_modulASP")));
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EmptyProjectDbContext>(options =>
        options.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
        var dbContext = scope.ServiceProvider.GetRequiredService<EmptyProjectDbContext>();
        dbContext.Database.Migrate(); // Wendet ausstehende Migrationen an
}

app.UseStaticFiles();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();