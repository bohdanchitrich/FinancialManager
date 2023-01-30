using Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace UI.Models.Categories;
[BindProperties]
public class CategoryAddModel : PageModel
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