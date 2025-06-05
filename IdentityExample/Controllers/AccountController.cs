using IdentityExample.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl)
    {
        var model = new LoginModel()
        {
            UserName = string.Empty,
            Password = string.Empty,
            ReturnUrl = returnUrl
        };
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // ModelState.AddModelError("UploadedFile", "Ungültige Datei.");
            // return View(model);
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager
                    .PasswordSignInAsync(
                        user,
                        model.Password,
                        false,
                        false
                        );
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "/");
                }
            }
            ModelState.AddModelError("", "Username oder Passwort ungültig.");
        }
        return View(model);
    }
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return View();
    }

    public async Task<IActionResult> AccessDenied(string returnUrl)
    {
        return View("AccessDenied", returnUrl);
    }
}