using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon_backend.Services;

using capicon_backend.Models;
namespace capicon_backend.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Editor")]
public class PostController : Controller
{
    private readonly int pageSize = 8;
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }
    [HttpGet]
    public async Task<IActionResult> Index(int page = 0)
    {
        // TODO: Пофиксить
        var posts = await _postService.SearchPostsAsync("", pageSize*page, pageSize);
        return View(posts);
    }

    [HttpGet]
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(NewsPostModel model)
    {
        await _postService.CreatePostAsync(model);
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _postService.DeletePost(id);
        return RedirectToAction(nameof(Index));
    }

}