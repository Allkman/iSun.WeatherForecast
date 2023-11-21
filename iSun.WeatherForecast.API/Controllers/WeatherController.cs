using iSun.WeatherForecast.SharedKernel.ApplicationContracts;
using iSun.WeatherForecast.SharedKernel.Models;
using Microsoft.AspNetCore.Mvc;

namespace iSun.WeatherForecast.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherDataApplicationService _service;


        private readonly ILogger<WeatherController> _logger;

        public WeatherController(IWeatherDataApplicationService service, ILogger<WeatherController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WeatherModel model)
        {
            try
            {
                await _service.PostAsync(model);

                return CreatedAtAction(nameof(Post), new { id = model.Id }, model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
