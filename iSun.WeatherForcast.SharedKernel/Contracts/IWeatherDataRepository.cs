using iSun.WeatherForecast.Core;

namespace iSun.WeatherForecast.SharedKernel.Contracts
{
    public interface IWeatherDataRepository
    {
        Task PostAsync(WeatherData data);
    }
}
