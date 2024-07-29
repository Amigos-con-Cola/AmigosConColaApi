using AmigosConCola.WebApi.Config.Auth;
using AmigosConCola.WebApi.Presentation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Throw;

namespace AmigosConCola.WebApi.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class AuthController : BaseApiController
{
    private readonly AuthConfig _authConfig;
    private readonly HttpClient _httpClient;

    public AuthController(
        HttpClient httpClient,
        IOptions<AuthConfig> authConfig)
    {
        _httpClient = httpClient;
        _authConfig = authConfig.Value;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var payload = new Dictionary<string, string>
        {
            { "grant_type", "password" },
            { "client_id", _authConfig.ClientId },
            { "username", request.Username },
            { "password", request.Password },
            { "client_secret", _authConfig.ClientSecret }
        };

        var result = await _httpClient.PostAsync(
            _authConfig.TokenEndpoint,
            new FormUrlEncodedContent(payload));

        if (!result.IsSuccessStatusCode)
            return Problem(statusCode: 401);

        var response = await result.Content.ReadFromJsonAsync<TokenResponse>();
        response.ThrowIfNull("Failed to deserialize keycloak response");

        return Ok(response);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest request)
    {
        var payload = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "refresh_token", request.RefreshToken },
            { "client_id", _authConfig.ClientId },
            { "client_secret", _authConfig.ClientSecret }
        };

        var result = await _httpClient.PostAsync(
            _authConfig.TokenEndpoint,
            new FormUrlEncodedContent(payload));

        if (!result.IsSuccessStatusCode)
            return Problem(statusCode: 401);

        var response = await result.Content.ReadFromJsonAsync<TokenResponse>();
        response.ThrowIfNull("Failed to deserialize keycloak response");

        return Ok(response);
    }
}