public class ConsumerBackgroundService : BackgroundService
{
    private readonly RabbitMQConsumerService _consumerService;

    public ConsumerBackgroundService(RabbitMQConsumerService consumerService)
    {
        _consumerService = consumerService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumerService.StartConsuming();
        return Task.CompletedTask;
    }
}
