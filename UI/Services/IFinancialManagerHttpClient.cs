namespace UI.Services;

public interface IFinancialManagerHttpClient
{
    Task<HttpResponseMessage> PostAsync(Uri request, HttpContent content);
    Task<HttpResponseMessage> PutAsync(Uri request, HttpContent content);
    Task<HttpResponseMessage> DeleteAsync(Uri request);
    Task<HttpResponseMessage> GetAsync(Uri request);

}