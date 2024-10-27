using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;

class Publisher
{
    public static async Task Main()
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        // Настройка параметров подключения
        var mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        // Подключение к MQTT-брокеру
        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
        Console.WriteLine("Publisher подключен к брокеру.");

        // Публикация сообщения
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("test/topic")
            .WithPayload("Hello from Publisher!")
            .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();

        await mqttClient.PublishAsync(message, CancellationToken.None);
        Console.WriteLine("Сообщение отправлено.");

        // Отключение клиента
        await mqttClient.DisconnectAsync();
        Console.WriteLine("Publisher отключен.");
    }
}
