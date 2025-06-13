namespace capicon.ApiExtentions;
public static class AreasSetup
{
    public static void AddAreas(this WebApplication app)
    {
        app.MapAreaControllerRoute(
            name: "admin",
            areaName: "Admin",
            pattern: "admin/{controller=Home}/{action=Index}/{id?}"
        );

        app.MapAreaControllerRoute(
            name: "news",
            areaName: "News",
            pattern: "news",
            defaults: new { controller = "Home", Action = "Index" }
        );

        app.MapAreaControllerRoute(
            name: "posts",
            areaName: "Post",
            pattern: "post/{id?}",
            defaults: new { controller = "Home", Action = "Index" }
        );

        app.MapAreaControllerRoute(
            name: "catalog",
            areaName: "Catalog",
            pattern: "catalog",
            defaults: new { controller = "Home", Action = "Index" }
        );

        app.MapAreaControllerRoute(
            name: "index",
            areaName: "Index",
            pattern: "/",
            defaults: new { controller = "Home", Action = "Index" }
        );

    }
}