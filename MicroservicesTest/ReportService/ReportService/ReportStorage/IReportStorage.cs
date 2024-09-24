
namespace ReportService
{
    public interface IReportStorage
    {
        void Add(Report report);
        IEnumerable<Report> Get();
    }
}