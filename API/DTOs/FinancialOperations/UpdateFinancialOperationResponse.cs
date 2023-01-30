using Domain.Categories;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.FinancialOperations;
[ExcludeFromCodeCoverage]
public class UpdateFinancialOperationResponse
{
    public int Id { get; set; }

    public Category? Category { get; set; }

    public DateTime Date { get; set; }

    public float Amount { get; set; }
}