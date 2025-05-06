using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capicon.Models;
using DataAccess;
using capicon.Services;

namespace capicon.Areas.News.Controllers;

[Area("News")]
public class NewsController : Controller
{
    private readonly ILogger<NewsController> _logger;
    private readonly PostsService _postService;
    public NewsController(ILogger<NewsController> logger, PostsService postService)
    {
        _logger = logger;
        _postService = postService;
    }

    public IActionResult Index(int id)
    {
        return View(_postService.GetPostsByOffset(id));
    }
}
