using Microsoft.AspNetCore.Components;
using UI.Models.Reports;
using UI.Models.Shared;

namespace UI.Pages.Report
{
    public partial class Report : ComponentBase
    {
        private ChooseReportModel ChooseReportModel { get; set; } = new()
        {
            ReportType = ReportType.None
        };

    }
}
