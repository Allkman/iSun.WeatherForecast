using iSun.WeatherForecast.Core;
using iSun.WeatherForecast.SharedKernel.Contracts;

namespace iSun.WeatherForecast.Infrastructure
{
    internal class WeatherDataService : IWeatherDataService
    {
        private readonly IWeatherDataRepository _repository;
        public WeatherDataService(IWeatherDataRepository repository)
        {
            _repository = repository;  
        }
        public Task PostAsync(WeatherData data)
        {
            return _repository.PostAsync(data);
        }
    }
}
