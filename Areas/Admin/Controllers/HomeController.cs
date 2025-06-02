using capicon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ProductService _productService;
    private readonly PostService _postService;
    private readonly AccountService _accountService;

    public HomeController(
        ProductService productService,
        PostService postService,
        AccountService accountService)
    {
        _productService = productService;
        _postService = postService;
        _accountService = accountService;
    }

    public async Task<IActionResult> Index()
    {

        var model = new DashboardViewModel
        {
            ProductCount = await _productService.GetCount(),
            PostCount = await _postService.GetCount(),
            UserCount = await _accountService.GetUserCount()
        };

        return View(model);
    }
}

public class DashboardViewModel
{
    public int ProductCount { get; set; }
    public int PostCount { get; set; }
    public int UserCount { get; set; }
}