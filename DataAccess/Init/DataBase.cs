using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Init;

public static class DbSetup
{
    public static async Task InitAdmin(WebApplication webApplication)
    {
        using (var scope = webApplication.Services.CreateScope())
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
    }
}