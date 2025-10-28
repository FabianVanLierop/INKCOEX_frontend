using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace INKCOEX_frontend.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public CustomAuthStateProvider(AuthService authService)
    {
        _authService = authService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _authService.GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymous);
        }

        var claims = ParseClaimsFromJwt(token).ToList();

        var roleClaim = claims.FirstOrDefault(c => c.Type == "role");
        if (roleClaim != null)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyAuthenticationChanged()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var parts = jwt.Split('.');
        if (parts.Length != 3)
            return Enumerable.Empty<Claim>();

        var payload = parts[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value?.ToString() ?? ""))
               ?? Enumerable.Empty<Claim>();
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}