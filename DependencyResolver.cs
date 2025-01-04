using URLShortener.Services.Contracts;
using URLShortener.Services.Implementations;

namespace URLShortener
{
    public static class DependencyResolver
    {
        public static IServiceCollection AddURLService(this IServiceCollection services)
        {
            services.AddTransient<IURLService, URLService>();


            return services;
        }
    }
}
