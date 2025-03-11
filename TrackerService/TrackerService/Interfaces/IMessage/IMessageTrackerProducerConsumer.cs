public interface IMessageTrackerProducerConsumer
{
    void StartConsuming();
    void SendMessage<T>(T message);
}