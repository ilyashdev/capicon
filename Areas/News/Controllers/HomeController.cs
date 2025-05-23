using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capicon.Models;
using DataAccess;
using capicon.Services;

namespace capicon.Areas.News.Controllers;

[Area("News")]
public class HomeController : Controller
{
    private readonly PostService _postService;
    public HomeController(ILogger<HomeController> logger, PostService postService)
    {
        _postService = postService;
    }

    public async Task<IActionResult> IndexAsync(int page = 0)
    {
        var posts = await _postService.SearchPosts("", page);
        return View(posts);
    }
}
