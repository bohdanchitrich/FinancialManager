using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Categories;

[ExcludeFromCodeCoverage]
public class AddCategoryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}