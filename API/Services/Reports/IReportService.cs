using API.DTOs.Reports;

namespace API.Services.Reports
{
    public interface IReportService
    {
        Task<GetDailyReportResponse> GetDailyReportAsync(GetDailyReportRequest dailyReportRequest);

        Task<GetDateReportResponse> GetDateReportAsync(GetDateReportRequest dateReportRequest);
    }
}
