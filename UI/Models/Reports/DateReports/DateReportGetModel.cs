using System.ComponentModel.DataAnnotations;

namespace UI.Models.Reports.DateReports;

public class DateReportGetModel
{
    [Required]
    public DateTime StartDateTime { get; set; }
    [Required]
    public DateTime EndDateTime { get; set; }
}