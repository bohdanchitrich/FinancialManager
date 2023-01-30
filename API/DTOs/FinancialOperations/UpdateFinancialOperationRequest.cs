using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.FinancialOperations;
[ExcludeFromCodeCoverage]
public class UpdateFinancialOperationRequest
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int CategoryId { get; set; }

    public DateTime Date { get; set; }

    public float Amount { get; set; }
}