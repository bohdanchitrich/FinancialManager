using AutoMapper;
using Microsoft.AspNetCore.Components;
using System.Text.Json;
using UI.Models;
using UI.Models.Categories;
using UI.Models.FinancialOperations;
using UI.Services.Category;
using UI.Services.FinancialOperation;

namespace UI.Pages.FinancialOperation
{
    public partial class FinancialOperationEditForm : ComponentBase
    {

        [Parameter]
        public int Id { get; set; }
        [Inject]
        private IFinancialOperationService? FinancialOperationService { get; set; }
        [Inject]
        private IMapper? Mapper { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        [Inject]
        private ICategoryService? CategoryService { get; set; }

        private CategorySelectModel? CategorySelectModel { get; set; } = new();

        private FinancialOperationUpdateModel _financialOperationUpdateModel = new();

        private string ErrorMessage = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            var response = await FinancialOperationService!.GetByIdAsync(Id);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadFromJsonAsync<string>();
                ErrorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;

            }
            _financialOperationUpdateModel = Mapper!.Map<FinancialOperationUpdateModel>(JsonSerializer
                .Deserialize<Domain.FinancialOperations.FinancialOperation>(content, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var response = await CategoryService!.GetAllAsync();
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadFromJsonAsync<string>();
                    ErrorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message!;
                    return;
                }
                CategorySelectModel = (await response.Content.ReadFromJsonAsync<CategorySelectModel>())!;
                StateHasChanged();
            }
        }

        private async Task HandleValidationRequestedAsync()
        {
            var response = await FinancialOperationService!.UpdateAsync(_financialOperationUpdateModel);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                ErrorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            NavigationManager!.NavigateTo("/operation");
        }


    }
}
