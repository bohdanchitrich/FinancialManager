using Domain.Categories;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Categories;

[ExcludeFromCodeCoverage]
public class GetPagedCategoryResponse
{
    public IList<Category> Categories { get; } = new List<Category>();
    public int TotalCount { get; set; }
}