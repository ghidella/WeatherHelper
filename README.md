# WeatherHelper

Este projeto é um bot de previsão do tempo que utiliza o Twilio para enviar mensagens via WhatsApp. O usuário envia seu CEP e recebe a previsão do tempo para sua localização.

## Funcionalidades

- Recebe mensagens via WhatsApp usando Twilio.
- Solicita o CEP do usuário.
- Obtém as coordenadas geográficas (latitude e longitude) a partir do CEP.
- Consulta a previsão do tempo para as coordenadas obtidas.
- Envia a previsão do tempo para o usuário via WhatsApp.

## APIs Utilizadas

- **Twilio API**: Para enviar e receber mensagens via WhatsApp.
  - [Twilio API Documentation](https://www.twilio.com/docs/whatsapp)
- **ViaCEP API**: Para obter informações de endereço a partir do CEP.
  - [ViaCEP API Documentation](https://viacep.com.br/)
- **WeatherAPI**: Para obter a previsão do tempo com base em coordenadas geográficas.
  - [WeatherAPI Documentation](https://www.weatherapi.com/docs/)

## Requisitos

- .NET SDK
- Conta no Twilio
- Chave de API do WeatherAPI

