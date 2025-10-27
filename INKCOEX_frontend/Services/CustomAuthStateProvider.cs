using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace INKCOEX_frontend.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public CustomAuthStateProvider(AuthService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _authService.GetTokenAsync();

        ClaimsIdentity identity;

        if (!string.IsNullOrEmpty(token))
        {
            // You can add more claims if you decode the token
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "User")
            }, "apiauth_type");
        }
        else
        {
            identity = new ClaimsIdentity(); // not authenticated
        }

        var user = new ClaimsPrincipal(identity);
        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}