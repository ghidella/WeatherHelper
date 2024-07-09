using System;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var twilioService = new TwilioService();
            
            // Simular o recebimento de uma mensagem inicial do cliente
            string testPhoneNumber = "SEU_NUMERO";
            string initialMessage = "Olá, quero a previsão do tempo.";

            Console.WriteLine("Recebendo mensagem inicial do cliente...");
            var response = twilioService.HandleIncomingMessage(testPhoneNumber, initialMessage);
            Console.WriteLine("Resposta do Twilio:");
            Console.WriteLine(response.ToString());

            // Esperar um tempo para simular a resposta do usuário com o CEP
            await Task.Delay(2000);

            // Simular o envio do CEP pelo cliente
            string testCep = "SEU_CEP";
            Console.WriteLine("Cliente enviando CEP...");
            var cepResponse = twilioService.HandleIncomingMessage(testPhoneNumber, testCep);
            Console.WriteLine("Resposta do Twilio:");
            Console.WriteLine(cepResponse.ToString());

            // Esperar um tempo para garantir que a mensagem seja processada e enviada
            await Task.Delay(10000);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
