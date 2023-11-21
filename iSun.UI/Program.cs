using iSun.UI;
using iSun.UI.Exceptions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

const int TimeInterval = 15;

var tokenResponse = await LoginAsync("isun", "passwrod");
var citiesResponse = await GetCities(tokenResponse.Data.Token);
Console.WriteLine("Cities from API");
foreach (var city in citiesResponse)
{
    Console.WriteLine(city);
}
Console.WriteLine("||--------------||");
var input = Console.ReadLine();
List<string> requestedCities = SplitStringInput(input);
Console.WriteLine("List of cities:");

foreach (var city in requestedCities)
{
    Console.WriteLine(city);
}

static List<string> SplitStringInput(string input)
{
    string[] cityArray = input.Split(',', StringSplitOptions.RemoveEmptyEntries);

    List<string> cities = new List<string>(cityArray.Length);
    foreach (var city in cityArray)
    {
        cities.Add(city.Trim());
    }

    return cities;
}

if (tokenResponse.Data != null)
{
    var cancellationTokenSource = new CancellationTokenSource();

    var task = Task.Run(async () =>
    {
        while (!cancellationTokenSource.Token.IsCancellationRequested)
        {
            foreach (var city in requestedCities)
            {
                WeatherViewModel weatherData = await GetWeatherDataAsync(city, tokenResponse.Data.Token);

                if (weatherData != null)
                {
                    Console.WriteLine($"City: {weatherData.City}");
                    Console.WriteLine($"Temperature: {weatherData.Temperature}");
                    Console.WriteLine($"Precipitation: {weatherData.Precipitation}");
                    Console.WriteLine($"WindSpeed: {weatherData.WindSpeed}");
                    Console.WriteLine($"Summary: {weatherData.Summary}");

                    await PostAsync(weatherData);
                }
                else
                {
                    Console.WriteLine("Failed to retrieve weather data.");
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(TimeInterval));
        }
    }, cancellationTokenSource.Token);

    Console.WriteLine("Press any key to stop requesting for weather data...");
    Console.ReadKey();
    Console.WriteLine();
    Console.WriteLine("The request for weather data was stopped.");
    cancellationTokenSource.Cancel();

    await task;
}
else
{
    Console.WriteLine("Login to weather API failed.");
}

async Task<Result<TokenResponse>> LoginAsync(string username, string password)
{
    var result = new Result<TokenResponse>();

    try
    {
        using (HttpClient client = new HttpClient())
        {
            string loginUrl = "https://weather-api.isun.ch/api/authorize";

            var loginData = new
            {
                username,
                password
            };

            string jsonData = JsonConvert.SerializeObject(loginData);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(loginUrl, content);

            if (response.IsSuccessStatusCode)
            {
                result.Data =  JsonConvert.DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());
                result.IsSuccess = true;
            }
            else
            {
                result.ErrorMessage = "Login failed.";
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        result.ErrorMessage = "An error occurred during login.";
    }

    return result;
}

async Task<ICollection<string>> GetCities(string token)
{
    string apiUrl = "https://weather-api.isun.ch/api/cities";

    using (HttpClient client = new HttpClient())
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage response = await client.GetAsync($"{apiUrl}");
        response.EnsureSuccessStatusCode();

        if (response.Content != null)
        {
            return JsonConvert.DeserializeObject<List<string>>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            return new List<string>(); //common practice in case of collections is to return an empty collection instead of null
        }
    }
}
async Task<WeatherViewModel> GetWeatherDataAsync(string city, string token)
{
    try
    {
        string apiUrl = "https://weather-api.isun.ch/api/weathers/";

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{city}");
            response.EnsureSuccessStatusCode(); //here it throws exception if false

            return JsonConvert.DeserializeObject<WeatherViewModel>(await response.Content.ReadAsStringAsync());
        }
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"HTTP Request Error: {ex.Message}");
        throw new HttpRequestFailedException("Error during HTTP request.", ex);
    }
    catch (JsonException ex)
    {
        Console.WriteLine($"JSON Deserialization Error: {ex.Message}");
        throw new JsonDeserializationException("Error during JSON deserialization.", ex);
    }
}

async Task PostAsync(WeatherViewModel model)
{
    var url = "https://localhost:7278/api/Weathers";

    using (HttpClient client = new HttpClient())
    {
        string jsonData = JsonConvert.SerializeObject(model);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"{url}", content);
        response.EnsureSuccessStatusCode();
    }
}