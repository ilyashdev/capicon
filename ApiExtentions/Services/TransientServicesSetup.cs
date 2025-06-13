using capicon.Services;

namespace capicon.ApiExtentions;

public static class TransientServicesSetup
{
    public static void AddTransientServices(this IServiceCollection services)
    {
        services.AddTransient<VisitorService>();
    }
}