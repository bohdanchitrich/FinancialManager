using API.DTOs.Reports;
using API.Services.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
        }
        /// <summary>
        /// Get report on date
        /// </summary>
        /// <param name="dailyReportRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> DailyReportAsync([FromBody] GetDailyReportRequest dailyReportRequest)
        {
            ArgumentNullException.ThrowIfNull(dailyReportRequest);
            var result = await _reportService.GetDailyReportAsync(dailyReportRequest);
            return Ok(result);
        }
        /// <summary>
        /// Get date period report
        /// </summary>
        /// <param name="dateReportRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> DateReportAsync([FromBody] GetDateReportRequest dateReportRequest)
        {
            ArgumentNullException.ThrowIfNull(dateReportRequest);
            var result = await _reportService.GetDateReportAsync(dateReportRequest);
            return Ok(result);
        }
    }
}
