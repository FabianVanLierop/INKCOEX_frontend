using Microsoft.JSInterop;

namespace INKCOEX_frontend.Services;

public class AuthService
{
    private readonly IJSRuntime _js;

    public AuthService(IJSRuntime js)
    {
        _js = js;
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
        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
    }

    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", "authToken");
    }
}