using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capicon.Models;
using DataAccess;

namespace capicon.Controllers;

public class NewsController : Controller
{
    private readonly ILogger<NewsController> _logger;
    private readonly CSDbContext _context;

    public NewsController(ILogger<NewsController> logger, CSDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View($"Your name");
    }

}
