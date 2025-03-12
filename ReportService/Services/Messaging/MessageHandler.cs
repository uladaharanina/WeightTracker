using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class MessageHandler : IMessageHandler
{
    private readonly IRabbitMQConnection _connection;
    private readonly ILogger<MessageHandler> _logger;
    private readonly Dictionary<string, Action<string>> _pendingCallbacks = new Dictionary<string, Action<string>>();

    public MessageHandler(IRabbitMQConnection connection, ILogger<MessageHandler> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public void SendMessage<T>(T message, Action<string> getResponse)
    {
        using var channel = _connection.Connection.CreateModel();
        channel.QueueDeclare(queue: "weight_request", durable: true, exclusive: false, autoDelete: false, arguments: null);

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        var correlationId = Guid.NewGuid().ToString();

        var properties = channel.CreateBasicProperties();
        properties.CorrelationId = correlationId;

        //Add to dictionary
        _pendingCallbacks[correlationId] = getResponse;

        channel.BasicPublish(exchange: "", routingKey: "weight_request", basicProperties: properties, body: body);
        _logger.LogInformation($"Sent new: {json} with id {properties.CorrelationId}");
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
            _logger.LogInformation($"Received message:");

            var correlationId = ea.BasicProperties.CorrelationId;
            Console.WriteLine("in response" + correlationId);

            // Check if there's a pending callback for this correlation ID
            if (_pendingCallbacks.ContainsKey(correlationId))
            {
                Console.WriteLine("Invocation@!");

                // Invoke the callback with the message received from the consumer
                _pendingCallbacks[correlationId](message);

                // Remove the callback once it's used
                _pendingCallbacks.Remove(correlationId);
            }

        };

        channel.BasicConsume(queue: "weight_response", autoAck: true, consumer: consumer);
    }

}