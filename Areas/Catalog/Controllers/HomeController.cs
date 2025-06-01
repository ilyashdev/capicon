using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Catalog.Controllers;
[Area("Catalog")]
public class HomeController : Controller
{

    public IActionResult Index(int id)
    {
        return View();
    }
}