using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class CreateInventoryItemRequest
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("main_ingredient")]
    public required string MainIngredient { get; set; }

    [JsonPropertyName("format")]
    public required string Format { get; set; }

    [JsonPropertyName("volume")]
    public required string Volume { get; set; }

    [JsonPropertyName("via")]
    public required string Via { get; set; }

    [JsonPropertyName("expiration_date")]
    public required DateOnly ExpirationDate { get; set; }

    [JsonPropertyName("laboratory")]
    public required string Laboratory { get; set; }

    [JsonPropertyName("origin")]
    public required string Origin { get; set; }

    [JsonPropertyName("status")]
    public required string Status { get; set; }

    [JsonPropertyName("entry_date")]
    public required DateOnly EntryDate { get; set; }

    [JsonPropertyName("box_id")]
    public required string BoxId { get; set; }

    [JsonPropertyName("stock")]
    public required string Stock { get; set; }

    [JsonPropertyName("kind")]
    public required string Kind { get; set; }
}