using iSun.WeatherForecast.Core;

namespace iSun.WeatherForecast.Core
{
    public class WeatherData : EntityId
    {
        public string City { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public int Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
