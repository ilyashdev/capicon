

using Microsoft.AspNetCore.Mvc;
using capicon_backend.Services;
namespace capicon_backend.Areas.News.Controllers;

[Area("News")]
public class NewsController : Controller
{
    private readonly PostService _postService;
    private readonly int pageSize = 8;
    public NewsController(ILogger<NewsController> logger, PostService postService)
    {
        _postService = postService;
    }

    public async Task<IActionResult> IndexAsync(int page = 0)
    {
        var posts = await _postService.SearchPostsAsync("", pageSize*page, pageSize);
        return View(posts);
    }
}