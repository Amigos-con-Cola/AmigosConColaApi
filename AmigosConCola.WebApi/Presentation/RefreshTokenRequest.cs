using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class RefreshTokenRequest
{
    [JsonPropertyName("refresh_token")]
    public required string RefreshToken { get; set; }
}