using System.Security.Cryptography;
using System.Text;
using FileServer.Models;
using FileServer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FileServer.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly UserFileRepository _repository;
    private readonly UserManager<AppUser> _userManager;

    public DashboardController(UserFileRepository repository, UserManager<AppUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Upload()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upload(UploadViewModel model)
    {
        if (!ModelState.IsValid || model.UploadedFile == null || model.UploadedFile.Length == 0)
        {
            ModelState.AddModelError("UploadedFile", "Ungültige Datei.");
            return View(model);
        }

        var originalFileName = Path.GetFileName(model.UploadedFile.FileName);
        var extension = Path.GetExtension(originalFileName);

        string hash;
        using (var sha = SHA256.Create())
        {
            var inputBytes = Encoding.UTF8.GetBytes($"{originalFileName}-{DateTime.UtcNow.Ticks}");
            var hashBytes = sha.ComputeHash(inputBytes);
            hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        var storedFileName = $"{hash}{extension}";
        var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload", storedFileName);

        if (System.IO.File.Exists(fullpath))
        {
            ModelState.AddModelError("FileName", "Filename already exists.");
            return View(model);
        }

        await using var fileStream = new FileStream(fullpath, FileMode.Create);
        await model.UploadedFile.CopyToAsync(fileStream);

        // Aktuellen Nutzer abrufen
        var user = await _userManager.GetUserAsync(User);

        // Datei in DB speichern
        var userFile = new UserFile
        {
            OriginalName = originalFileName,
            StoredName = storedFileName,
            FileSize = model.UploadedFile.Length,
            UploadDate = DateTime.UtcNow,
            UserId = user!.Id
        };

        await _repository.AddFileAsync(userFile);
        await _repository.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}