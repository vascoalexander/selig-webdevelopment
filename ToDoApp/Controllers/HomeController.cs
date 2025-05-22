using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Repositories;
using System.Linq;

namespace ToDoApp.Controllers;

public class HomeController : Controller
{
    private readonly ToDoRepository _repository;

    public HomeController(ToDoRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var todoItems = _repository.GetAll();
        return View(todoItems);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ToDoItem item)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(item);
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ToDoItem item)
    {
        if (ModelState.IsValid)
        {
            _repository.Update(item);
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var item = _repository.GetById(id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DelteConfirmed(int id)
    {
        _repository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel() { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}