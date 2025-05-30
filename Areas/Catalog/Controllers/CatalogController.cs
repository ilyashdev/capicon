using capicon_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace capicon_backend.Areas.Catalog.Controllers;

[Area("Catalog")]
public class CatalogController : Controller
{
    private readonly int pageSize = 30;
    private readonly ProductService _productService;

    public CatalogController(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index(int page)
    {
        var products = await _productService.SearchProducts("", pageSize*page, pageSize);
        return View(products);
    }
}