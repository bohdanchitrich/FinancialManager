using Domain.FinancialOperations;
using Domain.Shared;

namespace UI.Models.Reports;
[Serializable]
public class BaseReportViewModel
{
    public float TotalIncome
    {
        get
        {
            return Operations.Where(obj => obj.Category!.FinancialType == FinancialType.Income)
                .Sum(obj => obj.Amount);
        }
    }

    public float TotalOutcome
    {
        get
        {
            return Operations.Where(obj => obj.Category is { FinancialType: FinancialType.Expense })
                .Sum(obj => obj.Amount);
        }
    }

    public IList<FinancialOperation> Operations { get; set; } = new List<FinancialOperation>();
}