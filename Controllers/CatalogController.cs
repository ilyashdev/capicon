using Microsoft.AspNetCore.Mvc;
using DataAccess;

namespace capicon.Controllers;

public class CatalogController : Controller
{
    private readonly ILogger<CatalogController> _logger;
    private readonly CSDbContext _context;
    private const int DIFF = 8;
    public CatalogController(ILogger<CatalogController> logger, CSDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Route("catalog/{id:int?}")]
    public IActionResult Index(int id)
    {
        return View(_context.News.OrderBy(n => n.dateTime).Skip(id*DIFF).Take(DIFF).ToList());
    }


}
