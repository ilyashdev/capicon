using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using capicon.Services;

using capicon.Models;
using Microsoft.AspNetCore.Identity;
namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Editor")]
public class PostController(PostService postService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // TODO: Пофиксить
        var posts = await postService.SearchPosts("", 0);
        ViewBag.PostCount = posts.Count;
        return View(posts);
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PostModel model)
    {
        await postService.CreatePost(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await postService.DeletePost(id);
        return RedirectToAction(nameof(Index));
    }

}