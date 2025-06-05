using System.Diagnostics;
using DbToApp2.Models;
using DbToApp2.Models.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeDetective;
using MimeDetective.Definitions;

namespace DbToApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger,
            AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var articles = await _context.Articles
                .Include(a => a.Author)
                .ToListAsync();
            return View(articles);
        }

        [HttpGet]
        public async Task<IActionResult> CreateArticle() {
            await LoadViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                article.Created = DateTime.Now;
                _context.Articles.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadViewBag();
            return View(article);
        }

        public async Task LoadViewBag() {
            var authors = await _context.Authors.ToListAsync();
            ViewBag.Authors = new SelectList(authors, "Id", "FullName");

            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", "images");
            var files = Directory.GetFiles(path)
                .Select(f => Path.GetFileName(f));
            ViewBag.Images = new SelectList(files);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ImageUpload()
        {
            return View();
        }

        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> ImageUpload(UploadViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var extension = Path.GetExtension(model.UploadedFile.FileName);
        //         var filename = Path.GetFileName(model.FileName) + extension;
        //         var fullpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", filename);
        //         if (System.IO.File.Exists(fullpath))
        //         {
        //             ModelState.AddModelError("FileName", "Filename already exists.");
        //             return View();
        //         }
        //
        //         await using (var stream = new FileStream(fullpath, FileMode.Create))
        //         {
        //             await model.UploadedFile.CopyToAsync(stream);
        //         }
        //
        //         return RedirectToAction(nameof(Index));
        //     }
        //
        //     return View();
        // }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ImageUpload(UploadViewModel model)
        {
            if (!ModelState.IsValid || model.UploadedFile == null || model.UploadedFile.Length == 0)
            {
                ModelState.AddModelError("UploadedFile", "Ungültige Datei.");
                return View(model);
            }

            // Inhalt analysieren mit Mime-Detective
            using var memoryStream = new MemoryStream();
            await model.UploadedFile.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            var inspector = new ContentInspectorBuilder
            {
                Definitions = DefaultDefinitions.All()
            }.Build();

            var result = inspector.Inspect(fileBytes);

            if (result == null || !result.ByMimeType().Any())
            {
                ModelState.AddModelError("UploadedFile", "Unbekannter oder ungültiger Dateityp.");
                return View(model);
            }

            var mimeType = result.ByMimeType().First().MimeType;

            if (!mimeType.StartsWith("image/"))
            {
                ModelState.AddModelError("UploadedFile", "Nur Bilddateien sind erlaubt.");
                return View(model);
            }

            // Dateinamen + Endung setzen
            var extension = Path.GetExtension(model.UploadedFile.FileName);
            var filename = Path.GetFileName(model.FileName) + extension;
            var fullpath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot", "images", filename);

            if (System.IO.File.Exists(fullpath))
            {
                ModelState.AddModelError("FileName", "Filename already exists.");
                return View(model);
            }

            await using var fileStream = new FileStream(fullpath, FileMode.Create);
            await model.UploadedFile.CopyToAsync(fileStream);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult FileList()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            var files = Directory.GetFiles(path)
                .Select(Path.GetFileName)
                .ToList();
            return View(files);
        }

        public IActionResult Download(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                var filepath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot", "images", file);
                if (System.IO.File.Exists(filepath))
                {
                    var contType = "application/octet-stream";
                    return PhysicalFile(filepath, contType, file);
                }
            }

            return RedirectToAction(nameof(FileList));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}