using AspNetCoreRateLimit;
using URLShortener.Services.Contracts;
using URLShortener.Services.Implementations;

namespace URLShortener
{
    public static class StartUpResolver
    {
        public static IServiceCollection AddURLService(this IServiceCollection services)
        {
            services.AddTransient<IURLService, URLService>();

            return services;
        }

        public static IServiceCollection AddRateLimitServiceConfiguration(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.RealIpHeader = "X-Real-IP";
                options.ClientIdHeader = "X-ClientId";
                options.GeneralRules =
                [
                    new RateLimitRule
                    {
                        Endpoint = "POST:/Link/Shorten",
                        Period = "60s",
                        Limit = 60,
                    }
                ];
            });

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            services.AddInMemoryRateLimiting();
            return services;
        }
    }
}
