using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class LoginRequest
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("password")]
    public required string Password { get; set; }
}