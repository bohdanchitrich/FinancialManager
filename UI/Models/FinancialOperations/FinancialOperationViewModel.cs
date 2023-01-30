using Domain.FinancialOperations;

namespace UI.Models.FinancialOperations;
[Serializable]
public class FinancialOperationViewModel
{
    public IList<FinancialOperation>? FinancialOperations { get; set; }

    public int TotalCount { get; set; }
}