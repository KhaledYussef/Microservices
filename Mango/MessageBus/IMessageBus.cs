namespace MessageBus
{
    public interface IMessageBus
    {
        Task Publish<T>(T message, string queue_topic);
        void Subscribe<T>(Action<T> action);
    }
}
