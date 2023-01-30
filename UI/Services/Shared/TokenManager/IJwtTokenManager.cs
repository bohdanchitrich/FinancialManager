namespace UI.Services.Shared.TokenManager;

public interface IJwtTokenManager
{
    Task<bool> SetHttpAuthorizationTokenAsync(HttpClient httpClient);
    Task<bool> RefreshHttpAuthorizationTokenAsync(HttpClient httpClient);
}