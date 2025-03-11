using RabbitMQ.Client;

public class RabbitMQConnection : IRabbitMQConnection, IDisposable
{
    private IConnection? _connection;
    public IConnection Connection => _connection!; // shorthand for get { return _connection}

    public RabbitMQConnection()
    {

        InitilizeConnection();
    }

    private void InitilizeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        _connection = factory.CreateConnection();
    }


    public void Dispose()
    {
        _connection?.Dispose();
    }
}
