using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly IAccountService _accountService;

    public AdminController(
        ILogger<AdminController> logger,
        IAccountService accountService)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet]
    public IActionResult Users()
    {
        var users = _accountService.GetAllUsers();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> RemoveUser(string userId)
    {
        var (success, errors) = await _accountService.DeleteUserAsync(userId);
        if (success)
        {
            TempData["Success"] = "Пользователь удалён.";
        }
        else
        {
            TempData["Error"] = string.Join("; ", errors);
        }
        return RedirectToAction("Users");
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        ViewBag.Roles = new SelectList(new[] { "Admin", "Editor", "Stats" });
        return View(new AddUserModel());
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(AddUserModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _accountService.GetAllRolesAsync();
            return View(model);
        }

        var (success, errors) = await _accountService.RegisterUserAsync(model);

        if (success)
        {
            TempData["Success"] = "Пользователь добавлен.";
            return RedirectToAction("Users");
        }

        foreach (var error in errors)
            ModelState.AddModelError("", error);

        ViewBag.Roles = new SelectList(new[] { "Admin", "Editor", "Stats" });
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> ModifyUser(string userId)
    {
        var user = await _accountService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound();

        var roles = await _accountService.GetRolesAsync(user);
        var model = new ModifyUserModel
        {
            UserLogin = user.UserName!,
            Email = user.Email!,
            UserRole = roles.FirstOrDefault() ?? "",
        };

        ViewBag.Roles = new SelectList(new[] { "Admin", "Editor", "Stats" });
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ModifyUser(ModifyUserModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = new SelectList(new[] { "Admin", "Editor", "Stats" });
            return View(model);
        }

        var (success, errors) = await _accountService.UpdateUserAsync(model);
        if (success)
        {
            TempData["Success"] = "Пользователь обновлён.";
            return RedirectToAction("Users");
        }

        foreach (var error in errors)
            ModelState.AddModelError("", error);

        ViewBag.Roles = new SelectList(new[] { "Admin", "Editor", "Stats" });
        return View(model);
    }
}
