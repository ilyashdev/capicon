using capicon.Models.Visitors;
using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace capicon.Services;

public class VisitorService(CSDbContext context)
{
    public async Task AddVisitor()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var date = await context.DailyVisitors
            .Where(v => v.Date ==  today)
            .FirstOrDefaultAsync();
        if (date == null)
            await context.DailyVisitors.AddAsync(new DailyVisitors{VisitorsCount = 1, Date = today});
        else
        {
            date.VisitorsCount++;
            await context.SaveChangesAsync();
        }
    }
}