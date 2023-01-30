using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Categories;
using UI.Services.Category;

namespace UI.Pages.Category
{
    public partial class CategoryAddForm : ComponentBase
    {
        private CategoryAddModel CategoryAddModel { get; set; } = new();
        [Inject]
        private ICategoryService? CategoryService { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string _errorMessage = string.Empty;

        private async Task HandleValidationRequestedAsync()
        {
            var response = await CategoryService!.AddAsync(this.CategoryAddModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
            }
            NavigationManager?.NavigateTo("/category");
        }
    }
}
