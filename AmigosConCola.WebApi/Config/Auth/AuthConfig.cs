namespace AmigosConCola.WebApi.Config.Auth;

public class AuthConfig
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string BaseUrl { get; set; }
    public required string TokenEndpoint { get; set; }
}