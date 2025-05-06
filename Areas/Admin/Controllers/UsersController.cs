using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

using capicon.Models;
namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AccountService _accountService;

    public UserController(AccountService accountService, ILogger<HomeController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersCount = await _accountService.GetAllUsersAsync();
        ViewBag.UserCount = usersCount.Count;
        var users = await _accountService.GetAllUsersAsync();
        return View(users);
    }

    [HttpGet]

    public IActionResult AddUser()
    {
        ViewBag.Roles = _accountService.GetAllRoles();
        return View();
    }

    [HttpPost]
    
    public async Task<IActionResult> AddUser(UserSetFieldModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _accountService.GetAllRoles();
            return View(model);
        }

        var result = await _accountService.AddUserAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = _accountService.GetAllRoles();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new UserSetFieldModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Role = user.Roles.FirstOrDefault() ?? ""
        };

        ViewBag.Roles = _accountService.GetAllRoles();
        return View(model);
    }

    [HttpPost]
    
    public async Task<IActionResult> EditUser(UserSetFieldModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Roles = _accountService.GetAllRoles();
            return View(model);
        }

        var result = await _accountService.ModifyUserAsync(model);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            ViewBag.Roles = _accountService.GetAllRoles();
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }
    [HttpPost]

    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _accountService.RemoveUserAsync(id);
        if (!result.Succeeded)
        {
            TempData["Error"] = "Ошибка при удалении пользователя.";
        }

        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> UserDetails(string id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        return View(user);
    }

}