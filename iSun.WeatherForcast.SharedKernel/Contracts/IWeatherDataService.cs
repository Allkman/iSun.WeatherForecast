using iSun.WeatherForecast.Core;

namespace iSun.WeatherForecast.SharedKernel.Contracts
{
    public interface IWeatherDataService
    {
        Task PostAsync(WeatherData data);
    }
}
