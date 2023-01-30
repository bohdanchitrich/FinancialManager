using Domain.Categories;

namespace API.DTOs.FinancialOperations;

public class GetFinancialOperationResponse
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
    public DateTime Date { get; set; }
    public float Amount { get; set; }
}