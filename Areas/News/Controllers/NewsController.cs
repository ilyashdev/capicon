using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capicon.Models;
using DataAccess;
using capicon.Services;

namespace capicon.Areas.News.Controllers;

[Area("News")]
public class NewsController : Controller
{
    private readonly PostService _postService;
    public NewsController(ILogger<NewsController> logger, PostService postService)
    {
        _postService = postService;
    }

    public IActionResult Index(int id)
    {
        return View(_postService.GetPosts(id));
    }
}
