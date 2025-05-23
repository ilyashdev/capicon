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
        var posts = await postService.GetPostsAsync();
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
        await postService.AddPostAsync(model);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await postService.DeletePostAsync(id);
        return RedirectToAction(nameof(Index));
    }

}