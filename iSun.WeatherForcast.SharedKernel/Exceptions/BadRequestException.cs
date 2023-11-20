using iSun.WeatherForecast.Core;

namespace iSun.WeatherForecast.SharedKernel.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException() : base(iSunResources.BadRequest) { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception ex) : base(message, ex) { }
    }
}
