using UI.Models.Categories;

namespace UI.Services.Category;

public interface ICategoryService
{
    Task<HttpResponseMessage> GetAllAsync(int page, int pageSize);
    Task<HttpResponseMessage> GetAllAsync();
    Task<HttpResponseMessage> GetByIdAsync(int id);
    Task<HttpResponseMessage> DeleteAsync(int id);
    Task<HttpResponseMessage> UpdateAsync(CategoryUpdateModel categoryUpdateModel);
    Task<HttpResponseMessage> AddAsync(CategoryAddModel categoryAddModel);
}