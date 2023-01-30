using Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Categories;

public class Category
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public bool IsRegular { get; set; }
    [Required]
    public FinancialType FinancialType { get; set; }

}