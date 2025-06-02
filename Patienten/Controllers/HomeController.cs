using Microsoft.AspNetCore.Mvc;
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
}