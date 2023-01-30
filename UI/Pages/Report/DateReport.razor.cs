using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Reports.DateReports;
using UI.Services.Report;

namespace UI.Pages.Report
{
    public partial class DateReport : ComponentBase
    {
        private DateReportGetModel DateReportGetModel { get; set; } = new();
        [Inject]
        private IReportService? ReportService { get; set; }
        private DateReportViewModel? DateReportViewModel { get; set; }

        private string _errorMessage = String.Empty;

        private async Task HandleValidationRequestedAsync()
        {
            var response = await ReportService!.GetDateReportAsync(DateReportGetModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
                return;
            }
            DateReportViewModel = await response.Content.ReadFromJsonAsync<DateReportViewModel>();
        }
    }
}
