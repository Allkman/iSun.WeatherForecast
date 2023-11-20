using iSun.WeatherForecast.Core;
using iSun.WeatherForecast.SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSun.WeatherForecast.Application
{
    internal static class ModelToEntityMapper
    {
        internal static WeatherData ToEntity(this WeatherModel model)
        {
            return new WeatherData()
            {
                Id = model.Id = Guid.NewGuid(), //Id could also be set in the db project in table creation script: uniqueidentifier NEWID()
                City = model.City,
                Temperature = model.Temperature,
                Precipitation = model.Precipitation,
                WindSpeed = model.WindSpeed,
                Summary = model.Summary
            };
        }
    }
}
