using Domain.Categories;

namespace UI.Models.Categories;
[Serializable]
public class CategoryViewModel
{
    public IList<Category>? Categories { get; set; }
    public int TotalCount { get; set; }
}