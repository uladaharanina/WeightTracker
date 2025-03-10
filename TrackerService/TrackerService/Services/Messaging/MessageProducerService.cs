using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class MessageProducerService
{
    private readonly string _queueName = "weight-entry-queue";
}