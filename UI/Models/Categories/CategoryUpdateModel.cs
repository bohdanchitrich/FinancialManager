using Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.Categories;

public class CategoryUpdateModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(10)]
    public string? Name { get; set; }
    [Required]
    [StringLength(50)]
    public string? Description { get; set; }
    [Required]
    public bool IsRegular { get; set; }
    [Required]
    public FinancialType FinancialType { get; set; }
}