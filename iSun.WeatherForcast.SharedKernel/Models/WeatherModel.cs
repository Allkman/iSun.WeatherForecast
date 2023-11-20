namespace iSun.WeatherForecast.SharedKernel.Models
{
    public class WeatherModel
    {
        public Guid Id { get; set; }
        public string City { get; set; } = string.Empty;
        public int Temperature { get; set; }
        public int Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
