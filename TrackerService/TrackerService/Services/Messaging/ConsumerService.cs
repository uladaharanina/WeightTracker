using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class RabbitMQConsumerService : IMessageTrackerProducerConsumer
{
    private readonly IRabbitMQConnection _connection;
    private readonly ILogger<RabbitMQConsumerService> _logger;

    public RabbitMQConsumerService(IRabbitMQConnection connection, ILogger<RabbitMQConsumerService> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public void StartConsuming()
    {
        var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare("weight_request", durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Received message: {message}");

            // Process the message (e.g., call your service to handle the message)
            SendMessage("I got it, now I send it to you!");
        };

        channel.BasicConsume(queue: "weight_request", autoAck: true, consumer: consumer);
    }

    public void SendMessage<T>(T message)
    {

        using var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare(queue: "weight_response", durable: true, exclusive: false, autoDelete: false, arguments: null);
        //Send response
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        channel.BasicPublish(exchange: "", routingKey: "weight_response", body: body);
        Console.WriteLine($"Sent new: {json}");
    }

}
