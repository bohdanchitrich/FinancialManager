using System.Text;
using System.Text.Json;
using UI.Models.FinancialOperations;

namespace UI.Services.FinancialOperation;

public class FinancialOperationService : IFinancialOperationService
{
    private readonly IFinancialManagerHttpClient _financialManagerHttpClient;

    public FinancialOperationService(IFinancialManagerHttpClient financialManagerHttpClient)
    {
        _financialManagerHttpClient = financialManagerHttpClient ?? throw new ArgumentNullException(nameof(financialManagerHttpClient));
    }

    public Task<HttpResponseMessage> AddAsync(FinancialOperationAddModel financialOperationAddModel)
    {
        ArgumentNullException.ThrowIfNull(financialOperationAddModel);
        var content = new StringContent(JsonSerializer.Serialize(financialOperationAddModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PostAsync(new Uri("FinancialOperation", UriKind.Relative), content);
    }

    public Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return _financialManagerHttpClient.DeleteAsync(new Uri($"FinancialOperation?id={id}", UriKind.Relative));
    }

    public Task<HttpResponseMessage> GetAllAsync(int page, int pageSize)
    {
        return _financialManagerHttpClient.GetAsync(new Uri($"FinancialOperation/GetPaged?page={page}&pageSize={pageSize}", UriKind.Relative));

    }

    public Task<HttpResponseMessage> GetByIdAsync(int id)
    {
        return _financialManagerHttpClient.GetAsync(new Uri($"FinancialOperation?id={id}", UriKind.Relative));
    }

    public Task<HttpResponseMessage> UpdateAsync(FinancialOperationUpdateModel financialOperationUpdateModel)
    {
        ArgumentNullException.ThrowIfNull(financialOperationUpdateModel);
        var content = new StringContent(JsonSerializer.Serialize(financialOperationUpdateModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PutAsync(new Uri("FinancialOperation", UriKind.Relative), content);
    }


}