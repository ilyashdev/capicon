using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

using capicon.Areas.Admin.Models;
namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller {
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}