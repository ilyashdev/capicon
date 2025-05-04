using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace capicon.Controllers;

public class PostController : Controller
{
    private readonly CSDbContext _context;
    private const int DIFF = 8;
    public PostController(CSDbContext context)
    {
        _context = context;
    }

    [Route("post/{id:int?}")]
    public IActionResult Index(int id)
    {
        return View(_context.News.First(n => n.Id == id));
    }


}
