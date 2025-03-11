using RabbitMQ.Client;

public interface IRabbitMQConnection
{
    IConnection Connection { get; }
}