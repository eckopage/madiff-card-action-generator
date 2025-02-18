using CardService.Application.Interfaces;
using CardService.Infrastructure.Services;

namespace CardService.Infrastructure
{
    public static class DiContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICardService, CardServiceImplementation>();
            return services;
        }
    }
}