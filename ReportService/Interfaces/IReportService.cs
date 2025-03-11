public interface IReportService
{

    public Task<WeeklyReportDTO> GenerateWeeklyReport();
    public Task GenerateMonthlyReport();
}