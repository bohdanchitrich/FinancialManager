using API.DTOs.FinancialOperations;

namespace API.Services.FinancialOperations
{
    public interface IFinancialOperationService
    {
        Task<AddFinancialOperationResponse> AddNewAsync(AddFinancialOperationRequest addFinancialOperationRequest);

        Task<GetPagedFinancialOperationResponse> GetPagedAsync(int page, int pageSize);

        Task<GetFinancialOperationResponse> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task<UpdateFinancialOperationResponse> UpdateAsync(UpdateFinancialOperationRequest updateFinancialOperationRequest);
    }
}
