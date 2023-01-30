using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Categories;
using UI.Models.FinancialOperations;
using UI.Services.Category;
using UI.Services.FinancialOperation;

namespace UI.Pages.FinancialOperation
{
    public partial class FinancialOperationAddForm : ComponentBase
    {
        private FinancialOperationAddModel FinancialOperationAddModel = new();
        [Inject]
        private IFinancialOperationService? FinancialOperationService { get; set; }
        [Inject]
        private ICategoryService? CategoryService { get; set; }

         private CategorySelectModel? CategorySelectModel { get; set; }  =new ();
        [Inject]
        private NavigationManager? NavigationManager { get; set; }

        private string _errorMessage = string.Empty;


        protected override async Task OnInitializedAsync()
        {
            var response = await CategoryService!.GetAllAsync();
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
                return;
            }
            CategorySelectModel = (await response.Content.ReadFromJsonAsync<CategorySelectModel>())!;
        }


        private async Task HandleValidationRequestedAsync()
        {
            var response = await FinancialOperationService!.AddAsync(FinancialOperationAddModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
                return;
            }
            NavigationManager?.NavigateTo("/operation");
        }
    }
}
