using UI.Models.Reports.DailyReports;
using UI.Models.Reports.DateReports;

namespace UI.Services.Report;

public interface IReportService
{
    Task<HttpResponseMessage> GetDailyReportAsync(DailyReportGetModel dailyReportGetModel);
    Task<HttpResponseMessage> GetDateReportAsync(DateReportGetModel dateReportGetModel);
}