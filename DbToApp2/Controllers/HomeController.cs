using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DbToApp2.Models;
using DbToApp2.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DbToApp2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var articles = _context.Articles
            .Include(a => a.Author)
            .ToList();
        return View(articles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}