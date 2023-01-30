using Domain.FinancialOperations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.FinancialOperations;
[ExcludeFromCodeCoverage]
public class GetPagedFinancialOperationResponse
{
    public IList<FinancialOperation> FinancialOperations { get; } = new List<FinancialOperation>();

    public int TotalCount { get; set; }
}