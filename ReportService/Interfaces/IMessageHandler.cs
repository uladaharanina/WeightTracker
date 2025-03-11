public interface IMessageHandler
{

    void SendMessage<T>(T message);
    void StartConsuming();

}