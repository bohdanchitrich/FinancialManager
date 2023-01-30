using Domain.Categories;

namespace API.DTOs.Categories;

public class GetAllCategoryResponse
{
    public IList<Category> Categories { get; } = new List<Category>();
}