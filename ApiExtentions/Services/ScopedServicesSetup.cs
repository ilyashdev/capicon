using capicon.Services;
namespace capicon.ApiExtentions;
public static class ScopedServicesSetup
{
    public static void AddScopedService(this IServiceCollection services)
    {
        services.AddScoped<AccountService>();
        services.AddScoped<ProductService>();
        services.AddScoped<PostService>();
        services.AddScoped<ImageService>();
        services.AddScoped<VisitorService>();
    }
}