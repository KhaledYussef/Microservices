namespace EmailAPI
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
