
using Plain.RabbitMQ;

using System.Text.Json;

namespace ReportService
{
    public class ReportCollector : IHostedService
    {
        private readonly ISubscriber _subscriber;
        private readonly IReportStorage _reportStorage;
        public ReportCollector(ISubscriber subscriber,
            IReportStorage reportStorage)
        {
            _subscriber = subscriber;
            _reportStorage = reportStorage;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(ProcessMsg);
            return Task.CompletedTask;
        }

        private bool ProcessMsg(string message, IDictionary<string,object> headers)
        {
            Console.WriteLine($"Received: {message}");
            var report = JsonSerializer.Deserialize<Report>(message);
            _reportStorage.Add(report);
            return true;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
