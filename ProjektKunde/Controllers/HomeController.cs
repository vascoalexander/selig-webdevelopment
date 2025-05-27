using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjektKunde.Data;
using ProjektKunde.Models;

namespace ProjektKunde.Controllers;

public class HomeController : Controller
{
    private AppRepository _repository;

    public HomeController(AppRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _repository.GetAllProjects();
        return View(projects);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadViewBag();
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project project)
    {
        await LoadViewBag();
        if (ModelState.IsValid)
        {
            await _repository.Add(project);
            return RedirectToAction(nameof(Index));
        }

        return View(project);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _repository.GetProjectById(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var projectToDelete = await _repository.GetProjectById(id);
        if (projectToDelete == null)
        {
            return NotFound();
        }

        await _repository.Delete(projectToDelete);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var project = await _repository.GetProjectById(id.Value);
        if (project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, Project project)
    {
        if (id != project.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            await _repository.Update(project);
            return RedirectToAction();
        }

        return View(project);
    }

    private async Task LoadViewBag()
    {
        ViewBag.Customers = new SelectList(
            await _repository.GetAllCustomers(),
            nameof(Customer.Id),
            nameof(Customer.Company)
        );
    }
}

