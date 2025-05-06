using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

using capicon.Areas.Admin.Models;
namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AccountService _accountService;

    public HomeController(AccountService accountService, ILogger<HomeController> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}