using iSun.WeatherForecast.Core;

namespace iSun.WeatherForecast.Core
{
    public abstract class EntityId : Entity
    {
        public Guid? Id { get; set; }
    }
}
