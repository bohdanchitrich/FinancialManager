using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Text.Json;
using UI.Models;
using UI.Models.FinancialOperations;
using UI.Services.FinancialOperation;

namespace UI.Pages.FinancialOperation
{
    public partial class FinancialOperationComponent : ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState>? AuthenticationStateTask { get; set; }
        [Inject]
        private IFinancialOperationService? FinancialOperationService { get; set; }
        [Inject]
        private IConfigurationRoot? ConfigurationManager { get; set; }
        [Inject]
        private NavigationManager? NavigationManager { get; set; }
        private FinancialOperationViewModel? FinancialOperationViewModel { get; set; }

        private string _errorMessage = String.Empty;

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
            var response = await FinancialOperationService!.GetAllAsync(page, _paginationModel.PageSize);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            FinancialOperationViewModel = await response.Content.ReadFromJsonAsync<FinancialOperationViewModel>() ?? new();
            _paginationModel.Count = FinancialOperationViewModel.TotalCount;
        }


        private async Task DeleteAsync(int id)
        {
            var response = await FinancialOperationService!.DeleteAsync(id);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<string>();
                _errorMessage = JsonSerializer.Deserialize<ErrorDto>(content!)!.Message ?? "Error";
                return;
            }
            NavigationManager!.NavigateTo("/operation");
        }
    }
}
