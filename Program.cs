using capicon.Models;
using capicon.Services;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Login";
    options.AccessDeniedPath = "/Denied";
});

services.AddControllersWithViews()
    .AddRazorOptions(options =>
    {
        options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
    });

services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CSDbContext>()
    .AddDefaultTokenProviders();

services.AddDbContext<CSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

services.AddScoped<AccountService>();
services.AddScoped<PostService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CSDbContext>();
    await db.Database.MigrateAsync();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if (!userManager.Users.Any())
    {
        var roleNames = new[] { "Admin", "Editor", "Analytics" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var password = "Admin123!";
        var admin = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, password);


        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine($"Админ создан: {admin.UserName}/{admin.Email}/{password}");
        }
        else
        {
            Console.WriteLine("Ошибка при создании администратора:");
            foreach (var error in result.Errors)
                Console.WriteLine($"- {error.Description}");
        }
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "admin/{controller=Home}/{action=Index}/{id?}"
);

app.MapAreaControllerRoute(
    name: "news",
    areaName: "News",
    pattern: "news/{id?}",
    defaults: new { controller = "Home", Action = "Index" }
);

app.MapAreaControllerRoute(
    name: "posts",
    areaName: "Post",
    pattern: "post/{id?}",
    defaults: new { controller = "Home", Action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
