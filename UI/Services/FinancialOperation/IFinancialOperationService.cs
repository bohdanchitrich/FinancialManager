using UI.Models.FinancialOperations;

namespace UI.Services.FinancialOperation;

public interface IFinancialOperationService
{
    Task<HttpResponseMessage> GetAllAsync(int page, int pageSize);
    Task<HttpResponseMessage> GetByIdAsync(int id);
    Task<HttpResponseMessage> DeleteAsync(int id);
    Task<HttpResponseMessage> UpdateAsync(FinancialOperationUpdateModel financialOperationUpdateModel);
    Task<HttpResponseMessage> AddAsync(FinancialOperationAddModel financialOperationAddModel);
}