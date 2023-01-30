using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Reports.DailyReports;
using UI.Services.Report;

namespace UI.Pages.Report
{
    public partial class DailyReport : ComponentBase
    {

        private DailyReportViewModel? DailyReportViewModel { get; set; }
        private DailyReportGetModel DailyReportGetModel { get; set; } = new();
        [Inject]
        private IReportService? ReportService { get; set; }

        private string _errorMessage = String.Empty;

        private async Task HandleValidationRequestedAsync()
        {
            var response = await ReportService!.GetDailyReportAsync(DailyReportGetModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
                return;
            }
            DailyReportViewModel = await response.Content.ReadFromJsonAsync<DailyReportViewModel>();
        }

    }
}
