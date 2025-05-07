using Microsoft.AspNetCore.Mvc;
using capicon.Services;

namespace capicon.Areas.Posts.Controllers;

[Area("Post")]
public class HomeController : Controller
{
    private readonly PostService _postService;

    public HomeController(PostService postService)
    {
        _postService = postService;
    }
    public async Task<IActionResult> IndexAsync(int id)
    {
        var post = await _postService.GetPost(id);
        return View(post);
    }
}
