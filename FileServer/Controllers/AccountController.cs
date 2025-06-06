using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FileServer.Models;
using Microsoft.AspNetCore.Authorization;

namespace FileServer.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public IActionResult Login(string returnUrl = "/")
    {
        var model = new LoginModel
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
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View(model);
        }

        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user != null)
        {
            await _signInManager.SignOutAsync();
            var result = await _signInManager
                .PasswordSignInAsync(
                    model.UserName,
                    model.Password,
                    false,
                    false
                    );
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl ?? "/");
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return View();
    }

    public IActionResult AccessDenied(string returnUrl = "/")
    {
        return View("AccessDenied", returnUrl);
    }
}