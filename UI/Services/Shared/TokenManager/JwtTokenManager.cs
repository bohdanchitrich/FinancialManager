using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using UI.Models.Shared;

namespace UI.Services.Shared.TokenManager;

public class JwtTokenManager : IJwtTokenManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly HttpClient _httpClient = new();
    private readonly IList<KeyValuePair<string, string>> _keyValuePairs = new List<KeyValuePair<string, string>>
    {
        new ("grant_type", "refresh_token")
    };

    public JwtTokenManager(IHttpContextAccessor httpContextAccessor, IConfigurationRoot configurationRoot)
    {
        _httpContextAccessor = httpContextAccessor
                               ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        ArgumentNullException.ThrowIfNull(configurationRoot);
        _httpClient.BaseAddress = new Uri(configurationRoot["Jwt:Authority"] + "/protocol/openid-connect/token");
        _keyValuePairs.Add(new KeyValuePair<string, string>("client_id", configurationRoot["Jwt:ClientId"]));
    }

    public async Task<bool> RefreshHttpAuthorizationTokenAsync(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("refresh_token");
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        _keyValuePairs.Add(new KeyValuePair<string, string>("refresh_token", token));
        var result = await _httpClient.PostAsync(new Uri("", UriKind.Relative), new FormUrlEncodedContent(_keyValuePairs));
        if (!result.IsSuccessStatusCode)
        {
            return false;
        }
        var user = await result.Content.ReadFromJsonAsync<UserModel>();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user!.AccessToken);
        return true;
    }

    public async Task<bool> SetHttpAuthorizationTokenAsync(HttpClient httpClient)
    {
        ArgumentNullException.ThrowIfNull(httpClient);
        var token = await _httpContextAccessor.HttpContext!.GetTokenAsync("access_token");
        if (string.IsNullOrEmpty(token))
        {
            return false;
        }
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return true;
    }
}