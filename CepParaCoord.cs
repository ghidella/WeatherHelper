using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ViaCepResponse // Estrutura dos dados da API ViaCEP
{
    public string? Logradouro { get; set; } // ? nullable
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? Uf { get; set; }
}

public class NominatimResponse // Dados da API Nominatim
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class CepParaCoord
{
    private static readonly HttpClient client = new HttpClient(); // Client para requisicoes

    public static async Task<(double Lat, double Lon)> GetCoordinatesAsync(string cep) // async permite await
    {
        string viaCepUrl = $"https://viacep.com.br/ws/{cep}/json/";
        var viaCepResponse = await client.GetFromJsonAsync<ViaCepResponse>(viaCepUrl);

        if (viaCepResponse != null && !string.IsNullOrEmpty(viaCepResponse.Localidade))
        {
            string address = $"{viaCepResponse.Logradouro}, {viaCepResponse.Bairro}, {viaCepResponse.Localidade}, {viaCepResponse.Uf}, Brasil";
            string nominatimUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)"); // cabeçalho User-Agent
            var nominatimResponse = await client.GetFromJsonAsync<NominatimResponse[]>(nominatimUrl);

            if (nominatimResponse != null && nominatimResponse.Length > 0)
            {
                var location = nominatimResponse[0];
                return (location.Lat, location.Lon);
            }
        }
        throw new Exception("Não foi possível obter as coordenadas.");
    }
}