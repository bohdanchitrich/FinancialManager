using AutoMapper;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Categories;
using UI.Services.Category;

namespace UI.Pages.Category
{
    public partial class CategoryEditForm : ComponentBase
    {

        [Parameter]
        public int Id { get; set; }
        [Inject]
        private ICategoryService? CategoryService { get; set; }
        [Inject]
        private IMapper? Mapper { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private CategoryUpdateModel _categoryUpdateModel = new();

        private string _errorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var response = await CategoryService!.GetByIdAsync(Id);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;

            }
            _categoryUpdateModel = Mapper!.Map<CategoryUpdateModel>(JsonSerializer
                .Deserialize<Domain.Categories.Category>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            await base.OnInitializedAsync();

        }

        private async Task HandleValidationRequestedAsync()
        {
            var response = await CategoryService!.UpdateAsync(_categoryUpdateModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            NavigationManager!.NavigateTo("/category");
        }

    }
}
