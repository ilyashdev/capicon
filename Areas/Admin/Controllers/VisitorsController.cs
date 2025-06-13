using capicon.Models.Visitors;
using capicon.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace capicon.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Analytics")]
public class VisitorsController : Controller
{
    private readonly VisitorService _visitorService;

    public VisitorsController(VisitorService visitorService)
    {
        _visitorService = visitorService;
    }

    public async Task<IActionResult> Index(DateOnly? startDate, DateOnly? endDate)
    {
        var utcNow = DateTime.UtcNow;
        var start = startDate ?? DateOnly.FromDateTime(utcNow.Date); 
        var end = endDate ?? DateOnly.FromDateTime(utcNow);

        var visitors = await _visitorService.GetVisitorsByPeriod(start, end);

        ViewBag.StartDate = start.ToString("dd-MM-yyyy");
        ViewBag.EndDate = end.ToString("dd-MM-yyyy");

        return View(visitors);
    }


}