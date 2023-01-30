using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Reports;
[ExcludeFromCodeCoverage]
public class GetDailyReportRequest
{
    [Required]
    [DataType(DataType.Date)]
    public DateTime DateTime { get; set; }
}