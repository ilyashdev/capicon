using Microsoft.AspNetCore.Mvc;
using capicon.Services;

namespace capicon.Areas.Posts.Controllers;

public class PostsController : Controller
{
    private readonly PostsService _postService;

    public PostsController(PostsService postService)
    {
        _postService = postService;
    }
    public IActionResult Index(int id)
    {
        return View(_postService.GetPost(id));
    }


}
