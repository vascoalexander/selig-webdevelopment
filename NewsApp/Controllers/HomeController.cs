using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repositories;

namespace NewsApp.Controllers;

public class HomeController : Controller
{
    private AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var articles = _context.Articles
            .Include(a => a.Author);
        return View(articles);
    }

    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = _context.Articles
            .FirstOrDefault(a => a.Id == id);

        if (id == null)
        {
            return NotFound();
        }

        return View(article);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Article item)
    {
        LoadViewBag();
        if (ModelState.IsValid)
        {
            await _context.Add(item);
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    private void LoadViewBag()
    {
        ViewBag.Authors = new SelectList(
            _context.Authors,
            nameof(Author.Id),
            nameof(Author.Fullname));
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}