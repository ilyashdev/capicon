using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capicon.Models;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

namespace capicon.Controllers;

// [Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ILogger<AdminController> _logger;
    private readonly AccountService _accountService;

    public AdminController(AccountService accountService, ILogger<AdminController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpGet]
    
    public async Task<IActionResult> Users()
    {
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
    
    public async Task<IActionResult> AddUser(CreateUserModel model)
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

        return RedirectToAction(nameof(Users));
    }

    
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _accountService.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();

        var model = new ModifyUserModel
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
    
    public async Task<IActionResult> EditUser(ModifyUserModel model)
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

        return RedirectToAction(nameof(Users));
    }
    [HttpPost]

    public async Task<IActionResult> DeleteUser(string id)
    {
        var result = await _accountService.RemoveUserAsync(id);
        if (!result.Succeeded)
        {
            TempData["Error"] = "Ошибка при удалении пользователя.";
        }

        return RedirectToAction(nameof(Users));
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