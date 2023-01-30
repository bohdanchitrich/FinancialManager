using Domain.Categories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.FinancialOperations;

public class FinancialOperation
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    [Required]
    public virtual Category? Category { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public float Amount { get; set; }
}