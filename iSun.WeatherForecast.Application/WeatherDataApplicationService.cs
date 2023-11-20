using iSun.WeatherForecast.SharedKernel.ApplicationContracts;
using iSun.WeatherForecast.SharedKernel.Contracts;
using iSun.WeatherForecast.SharedKernel.Models;

namespace iSun.WeatherForecast.Application
{
    public class WeatherDataApplicationService : IWeatherDataApplicationService
    {
        private readonly IWeatherDataService _service;
        public WeatherDataApplicationService(IWeatherDataService service)
        {
            _service = service;
        }

        public async Task PostAsync(WeatherModel model)
        {
            var entity = model.ToEntity();// I can instead use Automapper here to map objects.

            await _service.PostAsync(entity);

        }
    }
}
