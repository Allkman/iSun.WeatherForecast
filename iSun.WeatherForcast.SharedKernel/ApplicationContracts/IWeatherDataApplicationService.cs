using iSun.WeatherForecast.SharedKernel.Models;

namespace iSun.WeatherForecast.SharedKernel.ApplicationContracts
{
    public interface IWeatherDataApplicationService
    {
        Task PostAsync(WeatherModel model);
    }
}
