using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public required string AccessToken { get; set; }

    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; set; }
}