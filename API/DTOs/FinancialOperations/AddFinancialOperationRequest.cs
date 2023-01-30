using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.FinancialOperations;
[ExcludeFromCodeCoverage]
public class AddFinancialOperationRequest
{
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public float Amount { get; set; }
}