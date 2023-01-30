namespace API.DTOs.Reports;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class GetDailyReportResponse : BaseReportResponse
{
    public string? DateOnly { get; set; }

}