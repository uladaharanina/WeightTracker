using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageHandler : IMessageHandler
{

    private readonly IRabbitMQConnection _connection;
    private readonly ILogger<MessageHandler> _logger;

    public MessageHandler(IRabbitMQConnection connection, ILogger<MessageHandler> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public void SendMessage<T>(T message)
    {
        using var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare(queue: "weight_request", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "weight_request", body: body);
        Console.WriteLine($"Sent new: {json}");
    }

    public void StartConsuming()
    {
        var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare("weight_response", durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Received message: {message}");

        };

        channel.BasicConsume(queue: "weight_response", autoAck: true, consumer: consumer);
    }

}