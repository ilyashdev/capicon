using capicon.Services;

namespace capicon.Middlewares;

public class VisitorMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, VisitorService visitorService)
    {
        var dailyToken = context.Request.Cookies["SsTk"];
        if (dailyToken == null)
        {
            context.Response.Cookies.Append("SsTk", Guid.NewGuid().ToString(), new CookieOptions()
            {
                Path = "/",
                HttpOnly = true,
                Secure = false,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });
            await visitorService.AddVisitor();
        }
        await next(context);
    }
}