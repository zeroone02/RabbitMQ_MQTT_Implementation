using System.Text;
using MQTTnet;
using MQTTnet.Client;

class Subscriber
{
    public static async Task Main()
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        // Настройка параметров подключения
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        // Обработчик для получения сообщений
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            var message = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine($"Получено сообщение: {message}");
            return Task.CompletedTask;
        };

        // Подключение к брокеру
        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        Console.WriteLine("Subscriber подключен к брокеру.");

        // Подписка на тему
        await mqttClient.SubscribeAsync("test/topic", MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce);
        Console.WriteLine("Подписан на test/topic.");

        // Ожидание для приема сообщений
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();

        // Отключение клиента
        await mqttClient.DisconnectAsync();
    }
}
