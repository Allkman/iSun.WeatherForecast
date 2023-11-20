using iSun.WeatherForecast.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace iSun.WeatherForecast.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WeatherData> WeatherDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
