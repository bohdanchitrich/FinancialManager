using System.Text;
using System.Text.Json;
using UI.Models.Reports.DailyReports;
using UI.Models.Reports.DateReports;

namespace UI.Services.Report;

public class ReportService : IReportService
{
    private readonly IFinancialManagerHttpClient _financialManagerHttpClient;

    public ReportService(IFinancialManagerHttpClient financialManagerHttpClient)
    {
        _financialManagerHttpClient = financialManagerHttpClient
            ?? throw new ArgumentNullException(nameof(financialManagerHttpClient));
    }

    public Task<HttpResponseMessage> GetDailyReportAsync(DailyReportGetModel dailyReportGetModel)
    {
        ArgumentNullException.ThrowIfNull(dailyReportGetModel);
        var content = new StringContent(JsonSerializer.Serialize(dailyReportGetModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PostAsync(new Uri("Report/DailyReport", UriKind.Relative), content);
    }

    public Task<HttpResponseMessage> GetDateReportAsync(DateReportGetModel dateReportGetModel)
    {
        ArgumentNullException.ThrowIfNull(dateReportGetModel);
        var content = new StringContent(JsonSerializer.Serialize(dateReportGetModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PostAsync(new Uri("Report/DateReport", UriKind.Relative), content);
    }
}