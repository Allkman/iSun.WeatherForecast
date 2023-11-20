using iSun.WeatherForecast.SharedKernel.Models;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net;
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
        public async Task PostWeatherData_WithGoodData()
        {
            var httpClient = _applicationFactory.CreateClient();
            var loginData = new
            {
                username = "isun",
                password = "passwrod"
            };
            string loginJsonData = JsonConvert.SerializeObject(loginData);
            var loginContent = new StringContent(loginJsonData, Encoding.UTF8, "application/json");
            var loginReponse = await httpClient.PostAsync(ApiEndpoints.authUserApiUrl, loginContent);

            Assert.NotNull(loginReponse);
            Assert.True(loginReponse.StatusCode == HttpStatusCode.OK);

            var weatherData = new WeatherModel()
            {
                Id = Guid.NewGuid(),
                City = "Vilnius",
                Temperature = 22,
                Precipitation = 15,
                WindSpeed = 10,
                Summary = "Warm"
            };

            string jsonData = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var weatherDataResponse = await httpClient.PostAsync(ApiEndpoints.getWeatherData, content);

            Assert.NotNull(weatherDataResponse);
            Assert.True(weatherDataResponse.StatusCode == HttpStatusCode.OK);
        }

        [Fact]
        public async Task PostWeatherData_WithBadData()
        {
            var httpClient = _applicationFactory.CreateClient();
            var loginData = new
            {
                username = "isun",
                password = "passwrod"
            };
            string loginJsonData = JsonConvert.SerializeObject(loginData);
            var loginContent = new StringContent(loginJsonData, Encoding.UTF8, "application/json");
            var loginReponse = await httpClient.PostAsync(ApiEndpoints.authUserApiUrl, loginContent);

            Assert.NotNull(loginReponse);
            Assert.True(loginReponse.StatusCode == HttpStatusCode.OK);

            var weatherData = new WeatherModel()
            {
                Id = Guid.NewGuid(),
                City = null,
                Temperature = 22,
                Precipitation = 15,
                WindSpeed = 10,
                Summary = null,
            };

            string jsonData = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var weatherDataResponse = await httpClient.PostAsync(ApiEndpoints.getWeatherData, content);

            Assert.NotNull(weatherDataResponse);
            Assert.True(weatherDataResponse.StatusCode == HttpStatusCode.OK);
        }
    }
}
