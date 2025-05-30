
using Microsoft.AspNetCore.Mvc;
using capicon_backend.Services;

namespace capicon_backend.Areas.Post.Controllers;

[Area("Post")]
public class PostController : Controller
{
    private readonly PostService _postService;
    
    public PostController(PostService postService)
    {
        _postService = postService;
    }
    public async Task<IActionResult> IndexAsync(int id)
    {
        var post = await _postService.GetPost(id);
        if (post == null)
            return NotFound();
        return View(post);
    }
}