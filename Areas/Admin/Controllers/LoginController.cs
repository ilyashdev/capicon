using capicon_backend.Models;
using capicon_backend.Models.User;
using capicon_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace capicon_backend.Areas.Admin.Controllers;

[Area("Admin")]
public class LoginController : Controller
{
    private readonly AccountService _accountService;

    public LoginController(AccountService accountService)
    {
        _accountService = accountService;
    }
    [HttpGet]
    public async Task<IActionResult> Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(UserAuthModel model)
    {
        var result = await _accountService.AuthorizeUserAsync(model);
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Неверный логин или пароль.");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _accountService.SignOutAsync();
        return RedirectToAction(nameof(Index));
    }
}