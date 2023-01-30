using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Categories;

[ExcludeFromCodeCoverage]
public class UpdateCategoryResponse
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsRegular { get; set; }
    public string? FinancialType { get; set; }
}