using iSun.WeatherForecast.API;
using iSun.WeatherForecast.SharedKernel.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace iSun.WeatherForecast.Tests
{
    public class WeatherControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        protected readonly CustomWebApplicationFactory<Program> _applicationFactory;

        public WeatherControllerIntegrationTests(CustomWebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public async Task LoginToApi_WithGoodCredentials()
        {
            var httpClient = _applicationFactory.CreateClient();

            var loginData = new
            {
                username = "isun",
                password = "passwrod"
            };
            string loginJsonData = JsonConvert.SerializeObject(loginData);
            var loginContent = new StringContent(loginJsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage loginResponse = await httpClient.PostAsJsonAsync(ApiEndpoints.authUserApiUrl, loginContent);

            Assert.NotNull(loginResponse);
            Assert.True(loginResponse.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostWeatherData_WithGoodData()
        {
            var httpClient = _applicationFactory.CreateClient();

            var weatherData = new WeatherModel()
            {
                Id = Guid.NewGuid(),
                City = "Vilnius",
                Temperature = 22,
                Precipitation = 15,
                WindSpeed = 10,
                Summary = "Warm"
            };

            string jsonData = JsonConvert.SerializeObject(weatherData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var weatherDataResponse = await httpClient.PostAsync(ApiEndpoints.getWeatherData, content);

            Assert.NotNull(weatherDataResponse);
            Assert.True(weatherDataResponse.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostWeatherData_WithBadData()
        {
            var httpClient = _applicationFactory.CreateClient();

            var weatherData = new WeatherModel()
            {
                Id = Guid.NewGuid(),
                City = null,
                Temperature = 22,
                Precipitation = 15,
                WindSpeed = 10,
                Summary = null,
            };

            string jsonData = JsonConvert.SerializeObject(weatherData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var weatherDataResponse = await httpClient.PostAsync(ApiEndpoints.getWeatherData, content);

            Assert.NotNull(weatherDataResponse);
            Assert.True(weatherDataResponse.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetCities_WithoutAuth()
        {
            var httpClient = _applicationFactory.CreateClient();

            var response = await httpClient.GetAsync("https://weather-api.isun.ch/api/cities'");
            Assert.NotNull(response);
            Assert.True(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
