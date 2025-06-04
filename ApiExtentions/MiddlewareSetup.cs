using capicon.Middlewares;
using capicon.Services;

namespace capicon.ApiExtentions;

public static class MiddlewareSetup
{
    public static void AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<VisitorMiddleware>();
    }
}