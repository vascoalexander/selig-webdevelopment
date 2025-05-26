using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.Repositories;

namespace NewsApp.Controllers;

public class HomeController : Controller
{
    private AppRepository _repository;
    public HomeController(AppRepository repository)
    {
        _repository = repository;
    }
    public async Task<IActionResult> Index()
    {
        var articles = await _repository.GetAllArticles();
        return View(articles);
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var article = await _repository.GetArticleById(id);
        if (article == null)
        {
            return NotFound();
        }
        return View(article);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadViewBag();
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Article item)
    {
        await LoadViewBag();
        if (ModelState.IsValid)
        {
            await _repository.Add(item);
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    private async Task LoadViewBag()
    {
        ViewBag.Authors = new SelectList(
            await _repository.GetAllAuthors(),
            nameof(Author.Id),
            nameof(Author.Fullname));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = await _repository.GetArticleById(id.Value);
        if (article == null)
        {
            return NotFound();
        }

        await LoadViewBag();
        return View(article);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Article item)
    {
        if (id != item.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _repository.Update(item);
            return RedirectToAction();
        }

        await LoadViewBag();
        return View(item);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var article = await _repository.GetArticleById(id.Value);
        return View(article);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var articleToDelete = await _repository.GetArticleById(id);
        if (articleToDelete == null)
        {
            return NotFound();
        }

        await _repository.Delete(articleToDelete);
        return RedirectToAction(nameof(Index));
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}