using Domain.Shared;

namespace API.DTOs.Categories;

public class GetCategoryResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsRegular { get; set; }
    public FinancialType FinancialType { get; set; }
}