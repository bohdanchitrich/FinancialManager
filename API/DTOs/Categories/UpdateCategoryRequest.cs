using Domain.Shared;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace API.DTOs.Categories;

[ExcludeFromCodeCoverage]
public class UpdateCategoryRequest
{
    [Required]
    public int Id { get; set; }

    [StringLength(10)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? Description { get; set; }

    public bool IsRegular { get; set; }

    public FinancialType FinancialType { get; set; }
}