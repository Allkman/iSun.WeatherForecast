using iSun.WeatherForecast.SharedKernel.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace iSun.WeatherForecast.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IWeatherDataService, WeatherDataService>();
            services.AddScoped<IWeatherDataRepository, WeatherDataRepository>();

            return services;
        }
    }
}
