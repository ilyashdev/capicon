using capicon_backend.Models.Catalog;
using capicon_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capicon_backend.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CatalogController : Controller
{
    private readonly int pageSize = 30;
    private readonly ProductService _productService;

    public CatalogController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int page)
    {
        var products = await _productService.SearchProducts("", pageSize * page, pageSize);
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Create() => View();
    
    [HttpPost]
    public async Task<IActionResult> Create(ProductModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _productService.CreateProduct(model);
        return RedirectToAction(nameof(Index));
    }
    
    // TODO: Реализовать другие методы
    // UPD: Сначала я думал забить, но сервис для продуктов оказался удобнее 
    // UPDUPD: нужны модели для этого контроллера, я сделаю тут ошибку чтобы вы увидели

    #error нужны модели для контроллера CatalogController в админке
    
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProduct(id);
        return View(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(ProductModel model)
    {
        await _productService.UpdateProduct(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteProduct(id);
            return RedirectToAction("Index");
    }
}