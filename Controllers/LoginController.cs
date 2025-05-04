using capicon.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
public class LoginController : Controller
{
    private readonly AccountService _accountService;

    public LoginController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("/login")]
    public IActionResult Index() => View();

    [HttpPost("/login")]
    public async Task<IActionResult> Index(string email, string password)
    {
        var success = await _accountService.SignInAsync(email, password);
        if (success)
            return RedirectToAction("Index", "Home");

        ModelState.AddModelError("", "Неверный логин или пароль.");
        return View();
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _accountService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
