using capicon.Areas.Admin.Models;
using capicon.Services;
using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
public class LoginController(AccountService accountService) : Controller
{
    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        var success = await accountService.SignInAsync(model.Email, model.Password);
        if (success)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Неверный логин или пароль.");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await accountService.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }
}