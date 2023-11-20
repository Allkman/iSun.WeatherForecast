using iSun.WeatherForecast.Application;
using iSun.WeatherForecast.SharedKernel.ApplicationContracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace iSun.WeatherForecast.Tests
{
    public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                services.AddScoped<IWeatherDataApplicationService, WeatherDataApplicationService>();

            });

            builder.UseEnvironment("Development");
        }
    }
}
