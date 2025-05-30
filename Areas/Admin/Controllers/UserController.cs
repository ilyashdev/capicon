using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon_backend.Services;

using capicon_backend.Models.User;

namespace capicon_backend.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController
    : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly AccountService _accountService;
    private readonly int pageSize = 16;
    
    public UserController(AccountService accountService, ILogger<UserController> logger)
    {
        _logger = logger;
        _accountService = accountService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page = 0)
    {
        var users = await _accountService.SearchUsersAsync("", pageSize*page, pageSize);
        return View(users);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        ViewBag.Roles = await _accountService.GetSystemRolesAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserModifyFieldModel model)
    {
        ViewBag.Roles = await _accountService.GetSystemRolesAsync();
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await _accountService.CreateUserAsync(model);
        if (result.Succeeded) return RedirectToAction(nameof(Index));
        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);

        return View(model);

    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new UserModifyFieldModel
        {
            Username = user.Username,
            Email = user.Email,
            Password = "",
            ConfirmPassword = "",
            Role = user.Role
        };

        ViewBag.Roles = await _accountService.GetSystemRolesAsync();
        return View(model);
    }

    [HttpPost]
    
    public async Task<IActionResult> Edit(UserModifyFieldModel model)
    {
        ViewBag.Roles = _accountService.GetSystemRolesAsync();
        if (!ModelState.IsValid)
            return View(model);

        var result = await _accountService.ModifyUserAsync(model);
        if (result.Succeeded) return RedirectToAction(nameof(Index));
        foreach (var error in result.Errors)
            ModelState.AddModelError("", error.Description);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _accountService.DeleteUserAsync(id);
        if (!result.Succeeded)
            TempData["Error"] = "Ошибка при удалении пользователя.";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var model = await _accountService.GetUserByIdAsync(id);
        if (model == null)
            return NotFound();

        return View(model);
    }

}