using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TrackerService.Interfaces;

public class RabbitMQConsumerService : IMessageTrackerProducerConsumer
{
    private readonly IRabbitMQConnection _connection;
    private readonly ILogger<RabbitMQConsumerService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMQConsumerService(IRabbitMQConnection connection, ILogger<RabbitMQConsumerService> logger, IServiceProvider serviceProvider)
    {
        _connection = connection;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void StartConsuming()
    {
        var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare("weight_request", durable: true, exclusive: false, autoDelete: false);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation($"Received message: {message}");

            var correlationalId = ea.BasicProperties.CorrelationId;
            Console.WriteLine("Received id" + correlationalId);
            // Process the message (e.g., call your service to handle the message)
            using (var scope = _serviceProvider.CreateScope())
            {
                var weightService = scope.ServiceProvider.GetRequiredService<IWeightService>();
                // Now you can use weightService
                var data = await weightService.GetWeights();
                SendMessage(data, correlationalId);
            }
            _logger.LogInformation($"Processed message: {message}\n Sending response:");

        };

        channel.BasicConsume(queue: "weight_request", autoAck: true, consumer: consumer);
    }

    public void SendMessage<T>(T message, string correlationId)
    {
        Console.WriteLine(correlationId);
        using var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare(queue: "weight_response", durable: true, exclusive: false, autoDelete: false, arguments: null);
        //Send response
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);
        var properties = channel.CreateBasicProperties();
        properties.CorrelationId = correlationId;

        channel.BasicPublish(exchange: "", routingKey: "weight_response", basicProperties: properties, body: body);
        Console.WriteLine($"Sent new: {json}");
    }

}
