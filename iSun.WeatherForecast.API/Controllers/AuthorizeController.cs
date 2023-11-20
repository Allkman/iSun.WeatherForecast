using Microsoft.AspNetCore.Mvc;

namespace iSun.WeatherForecast.API.Controllers
{
    public class AuthorizeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
