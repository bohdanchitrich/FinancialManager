using API.DTOs.Categories;

namespace API.Services.Categories
{
    public interface ICategoryService
    {
        Task<AddCategoryResponse> AddNewAsync(AddCategoryRequest categoryRequest);

        Task<GetPagedCategoryResponse> GetPagedAsync(int page, int pageSize);

        Task<GetAllCategoryResponse> GetAllCategoriesAsync();

        Task<GetCategoryResponse> GetByIdAsync(int id);

        Task DeleteAsync(int id);

        Task<UpdateCategoryResponse> UpdateAsync(UpdateCategoryRequest categoryRequest);
    }
}
