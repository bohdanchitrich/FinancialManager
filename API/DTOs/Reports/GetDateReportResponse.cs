using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Reports;
[ExcludeFromCodeCoverage]
public class GetDateReportResponse : BaseReportResponse
{

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

}