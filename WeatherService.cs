using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeatherResponse
{
    public Current? Current { get; set; }
}

public class Current
{
    public double Temp_C { get; set; }
    public Condition? Condition { get; set; }
}

public class Condition
{
    public string? Text { get; set; }
}

public class WeatherService
{
    private static readonly HttpClient client = new HttpClient(); // Client para reqs HTTP
    private const string apiKey = "XXXXX"; // Chave da API

    public static async Task<WeatherResponse> GetWeatherAsync(double lat, double lon) // Método assíncrono retorna Task contendo obj WeatherResponse
    {
        string weatherApiUrl = $"http://api.weatherapi.com/v1/current.json?key={apiKey}&q={lat},{lon}&aqi=no"; // Chama api
        var weatherResponse = await client.GetFromJsonAsync<WeatherResponse>(weatherApiUrl); // Get para API, "transforma" resposta JSON no objeto WeatherResponse 

        if (weatherResponse != null)
        {
            return weatherResponse;
        }
        throw new Exception("Não foi possível obter a previsão do tempo.");
    }
}
