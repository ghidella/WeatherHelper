using System;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.TwiML.Messaging;
using Twilio.Types;

public class TwilioService
{   
    // Auth
    private const string accountSid = "XXXXX;
    private const string authToken = "XXXXX";

    public TwilioService()
    {
        TwilioClient.Init(accountSid, authToken);
    }

    public void SendMessage(string to, string body) // Envia uma msg de texto via WhatsApp
    {
        var message = MessageResource.Create(
            body: body, // Mensagem
            from: new PhoneNumber("XXXXX"), // Número do sandbox do Twilio para WhatsApp
            to: new PhoneNumber(to) // Número do destinatário
        );

        Console.WriteLine($"Mensagem enviada com SID: {message.Sid}");
    }

    public async Task SendWeatherForecastAsync(string to, string cep) // Método assíncrono que envia a previsão do tempo para o número to
    {
        try
        {
            Console.WriteLine("Obtendo coordenadas para o CEP...");
            var coordinates = await CepParaCoord.GetCoordinatesAsync(cep);
            Console.WriteLine($"Coordenadas obtidas: Latitude {coordinates.Lat}, Longitude {coordinates.Lon}");

            Console.WriteLine("Obtendo previsão do tempo...");
            var weather = await WeatherService.GetWeatherAsync(coordinates.Lat, coordinates.Lon);
            Console.WriteLine("Previsão do tempo obtida.");

            string body = $"Previsão do tempo para hoje: {weather.Current?.Condition?.Text ?? "Sem descrição"}, Temp: {weather.Current?.Temp_C ?? 0}°C";

            SendMessage(to, body);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter previsão do tempo: {ex.Message}");
            SendMessage(to, "Não foi possível obter a previsão do tempo. Por favor, tente novamente mais tarde.");
        }
    }

    public MessagingResponse HandleIncomingMessage(string from, string body) // Lida com mensagens(body) recebidas de 'from'
    {
        var response = new MessagingResponse();

        if (body.All(char.IsDigit) && body.Length == 8)
        {
            // Iniciar a tarefa para enviar a previsão do tempo
            Task.Run(() => SendWeatherForecastAsync(from, body));
            response.Message("Processando seu CEP. Por favor, aguarde...");
        }
        else
        {
            response.Message("Por favor, envie o seu CEP (apenas números).");
        }

        return response;
    }
}
