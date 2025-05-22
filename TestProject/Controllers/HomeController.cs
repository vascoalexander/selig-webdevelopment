using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TestProject.Models;
using TestProject.ViewModels;

namespace TestProject.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ArtikelForm()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ArtikelForm(Position position)
    {
        if (ModelState.IsValid)
        {
            Repository.AddPosition(position);
            var vm = new AngelegtViewModel()
            {
                LastPosition = position,
                PositionAnzahl = Repository.Positions.Count()
            };
            return View("Angelegt", vm);
        }

        return View();
    }

    public IActionResult ArtikelAnsehen()
    {
        var liste = Repository.Positions
            .GroupBy(p => p.Geschaeft);
        return View(liste);
    }

    public IActionResult ArtikelLoeschen(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Repository.ArtikelLoeschen((int)id);
        return RedirectToAction(nameof(ArtikelAnsehen));
    }

    public IActionResult IncreaseArticleCount(int? id)
    {
        Repository.IncreaseArticleCount((int)id);
        return RedirectToAction(nameof(ArtikelAnsehen));
    }
    public IActionResult DecreaseArticleCount(int? id)
    {
        Repository.DecreaseArticleCount((int)id);
        return RedirectToAction(nameof(ArtikelAnsehen));
    }
}