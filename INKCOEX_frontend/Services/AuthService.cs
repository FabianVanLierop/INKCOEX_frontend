using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace INKCOEX_frontend.Services;

public class AuthService
{
    private readonly IJSRuntime _js;
    private readonly HttpClient _client;

    public AuthService(IJSRuntime js, HttpClient client)
    {
        _js = js;
        _client = client;
    }

    public async Task<string> GetTokenAsync()
    {
        return await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
    }

    public async Task<bool> IsLoggedInAsync()
    {
        var token = await GetTokenAsync();
        return !string.IsNullOrEmpty(token);
    }

    public async Task LoginAsync(string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
    }

    public async Task<AuthenticationHeaderValue> CreateAuthHeader()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        return new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task LogoutAsync()
    {
        _client.DefaultRequestHeaders.Authorization = null;
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
    }
}