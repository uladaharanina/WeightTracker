using RabbitMQ.Client;

public class RabbitMQConnection : IRabbitMQConnection
{
    private Lazy<Task<IConnection>>? _connection;
    public IConnection Connection => _connection.Value.Result;

    public RabbitMQConnection()
    {
        _connection = new Lazy<Task<IConnection>>(InitilizeConnection);
    }

    private async Task<IConnection> InitilizeConnection()
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        return await Task.Run(() => factory.CreateConnection());
    }



}
