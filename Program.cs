using capicon.ApiExtentions;
using DataAccess;
using DataAccess.Init;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
    });


services.AddControllersWithViews()
    .AddRazorOptions(options => { options.ViewLocationFormats.Add("/Views/Shared/{0}.cshtml"); });

services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<CSDbContext>()
    .AddDefaultTokenProviders();

services.AddDbContext<CSDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DataBase")));

services.AddScopedService();
services.AddTransientServices();

var app = builder.Build();

await DbSetup.InitAdmin(app);

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.AddMiddlewares();
app.MapStaticAssets();
app.AddAreas();

app.Run();

