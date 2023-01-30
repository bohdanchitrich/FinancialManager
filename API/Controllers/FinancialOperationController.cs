using API.DTOs.FinancialOperations;
using API.Services.FinancialOperations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialOperationController : ControllerBase
    {
        private readonly IFinancialOperationService _financialOperationService;

        public FinancialOperationController(IFinancialOperationService financialOperationService)
        {
            _financialOperationService = financialOperationService ?? throw new ArgumentNullException(nameof(financialOperationService));
        }
        /// <summary>
        /// Add financial operations to the system
        /// </summary>
        /// <param name="addFinancialOperationRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAsync([FromBody] AddFinancialOperationRequest addFinancialOperationRequest)
        {
            ArgumentNullException.ThrowIfNull(addFinancialOperationRequest);
            var result = await _financialOperationService.AddNewAsync(addFinancialOperationRequest);
            return Ok(result);
        }
        /// <summary>
        /// Get all available financial operations in system
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _financialOperationService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get financial operation by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromQuery] int id)
        {
            var result = await _financialOperationService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Delete financial operations from system
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync([FromQuery] int id)
        {
            await _financialOperationService.DeleteAsync(id);
            return Ok();
        }
        /// <summary>
        /// Update financial operations in system
        /// </summary>
        /// <param name="financialOperationRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateFinancialOperationRequest financialOperationRequest)
        {
            ArgumentNullException.ThrowIfNull(financialOperationRequest);
            var result = await _financialOperationService.UpdateAsync(financialOperationRequest);
            return Ok(result);
        }
    }
}
