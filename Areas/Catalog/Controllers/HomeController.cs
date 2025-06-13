using capicon.Services;
using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Catalog.Controllers;

[Area("Catalog")]
public class HomeController : Controller
{
    private readonly ProductService _productService;

    public HomeController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string query = "", int page = 0)
    {
        var products = await _productService.SearchProducts(query, page);
        return View(products);
    }
}