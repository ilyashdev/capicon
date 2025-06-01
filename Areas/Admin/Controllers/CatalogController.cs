using capicon.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CatalogController : Controller
{
    private readonly ProductService _productService;

    public CatalogController(ProductService productService)
    {
        _productService = productService;
    }
    public async Task<IActionResult> Index(int page = 0)
    {
        var products = await _productService.SearchProducts("", page);
        return View(products);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        await _productService.CreateProduct(model);
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProduct(id, 0);
        if (product == null)
            return NotFound();
        return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(ProductViewModel model)
    {
        await _productService.UpdateProduct(model);
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var product  = await _productService.GetProduct(id, 0);
        if (product == null)
            return NotFound();
        await _productService.DeleteProduct(product);
        return RedirectToAction(nameof(Index));
    }
}