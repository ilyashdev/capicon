using Microsoft.AspNetCore.Mvc;
using capicon.Services;

namespace capicon.Areas.Posts.Controllers;

[Area("Post")]
public class HomeController(PostService postService) : Controller
{
    public async Task<IActionResult> IndexAsync(int id)
    {
        var post = await postService.GetPost(id);
        if (post == null)
            return NotFound();
        return View(post);
    }
}
