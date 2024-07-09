using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ViaCepResponse // Estrutura dos dados da API ViaCEP
{
    public string? Logradouro { get; set; }
    public string? Bairro { get; set; }
    public string? Localidade { get; set; }
    public string? Uf { get; set; }
}

public class NominatimResponse // Dados da API Nominatim
{
    public double Lat { get; set; }
    public double Lon { get; set; }
}

public class Program
{
    private static readonly HttpClient client = new HttpClient(); // Classe para enviar e receber req HTTP

    public static async Task Main(string[] args) // async indica que contém código assíncrono, permitindo uso de 'await'
    {
        string cep = "03067000"; // Exemplo de CEP
        string viaCepUrl = $"https://viacep.com.br/ws/{cep}/json/"; 

        try
        {
            // GET para ViaCEP, resposta em json
            var viaCepResponse = await client.GetFromJsonAsync<ViaCepResponse>(viaCepUrl); // await p/ esperar conclusao das op assincronas sem bloq a thread principal

            if (viaCepResponse != null && !string.IsNullOrEmpty(viaCepResponse.Localidade))
            {
                string address = $"{viaCepResponse.Logradouro}, {viaCepResponse.Bairro}, {viaCepResponse.Localidade}, {viaCepResponse.Uf}, Brasil";
                Console.WriteLine($"Endereço: {address}");

                // Obter coordenadas usando Nominatim
                string nominatimUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";

                // Adicionar User-Agent
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

                var nominatimResponse = await client.GetFromJsonAsync<NominatimResponse[]>(nominatimUrl);

                if (nominatimResponse != null && nominatimResponse.Length > 0)
                {
                    var location = nominatimResponse[0];
                    Console.WriteLine($"Latitude: {location.Lat}, Longitude: {location.Lon}");
                }
                else
                {
                    Console.WriteLine("Não foi possível obter as coordenadas.");
                }
            }
            else
            {
                Console.WriteLine("Não foi possível obter o endereço.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
