namespace ReportService
{
    public class ReportStorage : IReportStorage
    {
        private readonly IList<Report> _reports = [];
        public void Add(Report report)
        {
            _reports.Add(report);

        }

        public IEnumerable<Report> Get()
        {
            return _reports;
        }
    }

    public class Report
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Count { get; set; }


    }
}
