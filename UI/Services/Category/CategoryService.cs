using System.Text;
using System.Text.Json;
using UI.Models.Categories;

namespace UI.Services.Category;

public class CategoryService : ICategoryService
{

    private readonly IFinancialManagerHttpClient _financialManagerHttpClient;

    public CategoryService(IFinancialManagerHttpClient financialManagerHttpClient)
    {
        _financialManagerHttpClient = financialManagerHttpClient;
    }

    public Task<HttpResponseMessage> AddAsync(CategoryAddModel categoryAddModel)
    {
        ArgumentNullException.ThrowIfNull(categoryAddModel);
        var content = new StringContent(JsonSerializer.Serialize(categoryAddModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PostAsync(new Uri("Category", UriKind.Relative), content);
    }

    public Task<HttpResponseMessage> DeleteAsync(int id)
    {
        return _financialManagerHttpClient.DeleteAsync(new Uri($"Category?id={id}", UriKind.Relative));
    }

    public Task<HttpResponseMessage> GetAllAsync(int page, int pageSize)
    {
        return _financialManagerHttpClient.GetAsync(new Uri($"Category/GetPaged?page={page}&pageSize={pageSize}", UriKind.Relative));
    }

    public Task<HttpResponseMessage> GetAllAsync()
    {
        return _financialManagerHttpClient.GetAsync(new Uri("Category/GetAll", UriKind.Relative));
    }

    public Task<HttpResponseMessage> GetByIdAsync(int id)
    {
        return _financialManagerHttpClient.GetAsync(new Uri($"Category?id={id}", UriKind.Relative));
    }

    public Task<HttpResponseMessage> UpdateAsync(CategoryUpdateModel categoryUpdateModel)
    {
        ArgumentNullException.ThrowIfNull(categoryUpdateModel);
        var content = new StringContent(JsonSerializer.Serialize(categoryUpdateModel), Encoding.UTF8, "application/json");
        return _financialManagerHttpClient.PutAsync(new Uri("Category", UriKind.Relative), content);
    }
}