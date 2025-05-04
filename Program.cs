using capicon.Services;
using DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
});

services.AddControllersWithViews();

services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CSDbContext>()
    .AddDefaultTokenProviders();

services.AddDbContext<CSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

services.AddScoped<AccountService>();

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

        var adminPass = "Admin123!";

        // Создание администратора
        var admin = new IdentityUser
        {
            UserName = "admin",
            Email = "admin@example.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(admin, adminPass);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "Admin");
            Console.WriteLine($"Админ создан: {admin.UserName}/{admin.Email}/{adminPass}");
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
