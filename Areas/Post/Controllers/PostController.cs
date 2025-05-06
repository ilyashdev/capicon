using Microsoft.AspNetCore.Mvc;
using capicon.Services;

namespace capicon.Areas.Posts.Controllers;

public class PostController : Controller
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }
    public IActionResult Index(int id)
    {
        return View(_postService.GetPost(id));
    }


}
