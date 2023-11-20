using iSun.WeatherForecast.Core;
using iSun.WeatherForecast.SharedKernel.Contracts;

namespace iSun.WeatherForecast.Infrastructure
{
    public class WeatherDataRepository : IWeatherDataRepository
    {
        private readonly ApplicationDbContext _context;
        public WeatherDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task PostAsync(WeatherData data)
        {
            _context.Add(data); 
            await _context.SaveChangesAsync();
        }
    }
}
