using System.Net;
using UI.Services.Shared.TokenManager;

namespace UI.Services;

public class FinancialManagerHttpClient : IFinancialManagerHttpClient
{

    private readonly HttpClient _httpClient;
    private readonly IJwtTokenManager _jwtTokenManager;
    public FinancialManagerHttpClient(IHttpClientFactory httpClientFactory, IJwtTokenManager jwtTokenManager)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _jwtTokenManager = jwtTokenManager
                           ?? throw new ArgumentNullException(nameof(jwtTokenManager));
    }

    public Task<HttpResponseMessage> DeleteAsync(Uri request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return HandleRequestAsync(() => _httpClient.DeleteAsync(request));
    }

    public Task<HttpResponseMessage> GetAsync(Uri request)
    {
        ArgumentNullException.ThrowIfNull(request);
        return HandleRequestAsync(() => _httpClient.GetAsync(request));
    }

    public Task<HttpResponseMessage> PostAsync(Uri request, HttpContent content)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(content);
        return HandleRequestAsync(() => _httpClient.PostAsync(request, content));
    }

    public Task<HttpResponseMessage> PutAsync(Uri request, HttpContent content)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(content);
        return HandleRequestAsync(() => _httpClient.PutAsync(request, content));

    }
    private async Task<HttpResponseMessage> HandleRequestAsync(Func<Task<HttpResponseMessage>> request)
    {
        ArgumentNullException.ThrowIfNull(request);
        await _jwtTokenManager.SetHttpAuthorizationTokenAsync(_httpClient);
        var response = await request.Invoke();
        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }
        if (await _jwtTokenManager.RefreshHttpAuthorizationTokenAsync(_httpClient))
        {
            return await request.Invoke();
        }
        return response;
    }
}