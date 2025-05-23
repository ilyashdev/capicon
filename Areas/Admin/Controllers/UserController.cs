using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

using capicon.Models;
namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController(AccountService accountService, ILogger<HomeController> logger)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersCount = await accountService.GetAllUsersAsync();
        ViewBag.UserCount = usersCount.Count;
        var users = await accountService.GetAllUsersAsync();
        return View(users);
    }

    [HttpGet]

    public async Task<IActionResult> Create()
    {
        ViewBag.Roles = await accountService.GetAllRolesAsync().ConfigureAwait(false);
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserSetFieldModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = await accountService.GetAllRolesAsync().ConfigureAwait(false);
            return View(model);
        }

        var result = await accountService.AddUserAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = await accountService.GetAllRolesAsync();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new UserSetFieldModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Roles.FirstOrDefault() ?? ""
        };

        ViewBag.Roles = await accountService.GetAllRolesAsync();
        return View(model);
    }

    [HttpPost]
    
    public async Task<IActionResult> Edit(UserSetFieldModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = accountService.GetAllRolesAsync();
            return View(model);
        }

        var result = await accountService.ModifyUserAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = await accountService.GetAllRolesAsync();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await accountService.RemoveUserAsync(id);
        if (!result.Succeeded)
        {
            TempData["Error"] = "Ошибка при удалении пользователя.";
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var user = await accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

}