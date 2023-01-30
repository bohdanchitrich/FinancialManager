using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Categories;

[ExcludeFromCodeCoverage]
public class AddCategoryRequest
{
    [Required]
    [StringLength(10)]
    public string? Name { get; set; }
    [StringLength(50)]
    public string? Description { get; set; }
    [Required]
    public bool IsRegular { get; set; }
    [Required]
    public FinancialType FinancialType { get; set; }
}