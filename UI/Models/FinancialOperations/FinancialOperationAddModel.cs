using System.ComponentModel.DataAnnotations;

namespace UI.Models.FinancialOperations;

public class FinancialOperationAddModel
{
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public float Amount { get; set; }
}