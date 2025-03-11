public class ConsumerBackgroundService : BackgroundService
{
    private readonly IMessageHandler _consumerService;

    public ConsumerBackgroundService(IMessageHandler consumerService)
    {
        _consumerService = consumerService;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumerService.StartConsuming();
        return Task.CompletedTask;
    }
}
