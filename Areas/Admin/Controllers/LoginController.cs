using capicon.Areas.Admin.Models;
using capicon.Services;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class LoginController : Controller
{
    private readonly AccountService _accountService;

    public LoginController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        var success = await _accountService.SignInAsync(model.Email, model.Password);
        if (success)
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
