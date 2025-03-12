public interface IMessageHandler
{

    void SendMessage<T>(T message, Action<string> callback);
    void StartConsuming();

}