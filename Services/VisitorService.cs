using capicon.Models.Visitors;
using capicon.Settings;
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

    public async Task<List<DailyVisitors>> GetVisitorsByPeriod(DateOnly startDate, DateOnly endDate)
    {
        var dailyVisitors =
            await context.DailyVisitors
                .Where(d => d.Date >= startDate && d.Date <= endDate)
                .ToListAsync();
        int points = dailyVisitors.Count / VisitorSettings.VISITOR_REZ;
        var visitorsByPeriod = new List<DailyVisitors>();
        for (int i = 0; i < VisitorSettings.VISITOR_REZ; i++)
        {
            var visitorsByPoint = dailyVisitors
                .Take((points * i)..(points * (i + 1)));
            var visitorCount = (int)visitorsByPoint.Average(v => v.VisitorsCount);    
            var visitorDate = DateOnly.FromDayNumber(
                (int)visitorsByPoint.Average(v => v.Date.DayNumber));
            visitorsByPeriod.Add(new DailyVisitors{VisitorsCount = visitorCount, Date = visitorDate});
        }
        return visitorsByPeriod;
    }
}