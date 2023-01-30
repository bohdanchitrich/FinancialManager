using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using UI.Models;
using UI.Models.Categories;
using UI.Services.Category;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UI.Pages.Category
{
    public partial class CategoryComponent : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; set; }
        [Inject]
        private ICategoryService? CategoryService { get; set; }
        [Inject]
        private IConfigurationRoot? ConfigurationManager { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        private CategoryViewModel? CategoryViewModel { get; set; }

        private string _errorMessage = string.Empty;

        private readonly PaginationModel _paginationModel = new();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }
            var user = (await AuthenticationStateTask!).User;
            if (!user.Identity!.IsAuthenticated)
            {
                return;
            }
            _paginationModel.PageSize = int.Parse(ConfigurationManager!["PageSize"], NumberStyles.Integer);
            await ShowModelsAsync(1);
            StateHasChanged();
        }

        private async Task ShowModelsAsync(int page)
        {
            var response = await CategoryService!.GetAllAsync(page, _paginationModel.PageSize);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            CategoryViewModel = await response.Content.ReadFromJsonAsync<CategoryViewModel>() ?? new();
            _paginationModel.Count = CategoryViewModel.TotalCount;
        }
        private async Task DeleteAsync(int id)
        {
            var response = await CategoryService!.DeleteAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            NavigationManager!.NavigateTo("/category", true);
        }

    }
}
