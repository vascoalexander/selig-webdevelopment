using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Patienten.Data;
using Patienten.Models;

namespace Patienten.Controllers;

public class HomeController : Controller
{
    private AppRepository _repository;

    public HomeController(AppRepository repository)
    {
        _repository = repository;
    }
    // Get
    public async Task<IActionResult> Index()
    {
        var termine = await _repository.GetAllTermine();
        return View(termine);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        await LoadViewBagAerzte();
        await LoadViewBagPatienten();

        var termin = await _repository.GetTerminById(id);
        if (termin == null)
        {
            return NotFound();
        }

        return View(termin);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Termin termin)
    {
        await LoadViewBagAerzte();
        await LoadViewBagPatienten();

        if (id != termin.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _repository.UpdateTermin(termin);
            return RedirectToAction();
        }

        return View(termin);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadViewBagAerzte();
        await LoadViewBagPatienten();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Termin termin)
    {
        await LoadViewBagAerzte();
        await LoadViewBagPatienten();

        if (ModelState.IsValid)
        {
            await _repository.AddTermin(termin);
            return RedirectToAction(nameof(Index));
        }

        return View(termin);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var termin = await _repository.GetTerminById(id);
        if (termin == null)
        {
            return NotFound();
        }

        return View(termin);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var terminToDelete = await _repository.GetTerminById(id);
        if (terminToDelete == null)
        {
            return NotFound();
        }

        await _repository.DeleteTermin(terminToDelete);
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadViewBagAerzte()
    {
        ViewBag.Aerzte = new SelectList(
            await _repository.GetAllAerzte(),
            nameof(Arzt.Id),
            nameof(Arzt.Fullname)
        );
    }

    private async Task LoadViewBagPatienten()
    {
        ViewBag.Patienten = new SelectList(
            await _repository.GetAllPatienten(),
            nameof(Patient.Id),
            nameof(Patient.Fullname)
        );
    }
}