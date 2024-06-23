using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace Tech.Web.Model.Account;

public class AuthService(
    HttpClient client,
    ILocalStorageService localStorage) : AuthenticationStateProvider
{
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await localStorage.GetItemAsync<string>("authToken");

        if (string.IsNullOrEmpty(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var claims = ParseClaimsFromJwt(token);
        var user =  new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

        return new AuthenticationState(user);
    }
    public async Task MarkUserAsAuthenticated(string token)
    {
        await localStorage.SetItemAsync("authToken", token);
        var claims = ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        await localStorage.RemoveItemAsync("authToken");
        var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymousUser)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue(ClaimTypes.Name, out object username);
        if (username != null)
        {
            claims.Add(new Claim(ClaimTypes.Name, username.ToString()));
        }

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
