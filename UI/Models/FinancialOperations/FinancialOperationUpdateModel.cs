using System.ComponentModel.DataAnnotations;

namespace UI.Models.FinancialOperations;

public class FinancialOperationUpdateModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public int CategoryId { get; set; }

    public DateTime Date { get; set; }

    public float Amount { get; set; }
}