using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Responses;

public class InventoryItemResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public required string Name { get; set; }

    [JsonPropertyName("ingrediente_principal")]
    public required string MainIngredient { get; set; }

    [JsonPropertyName("formato")]
    public required string Format { get; set; }

    [JsonPropertyName("volumen")]
    public required string Volume { get; set; }

    [JsonPropertyName("via")]
    public required string Via { get; set; }

    [DataType(DataType.Date)]
    [JsonPropertyName("fecha_vencimiento")]
    public required DateOnly ExpirationDate { get; set; }

    [JsonPropertyName("laboratorio")]
    public required string Laboratory { get; set; }

    [JsonPropertyName("origen")]
    public required string Origin { get; set; }

    [JsonPropertyName("estado")]
    public required string Status { get; set; }

    [DataType(DataType.Date)]
    [JsonPropertyName("fecha_registro")]
    public required DateOnly EntryDate { get; set; }

    [JsonPropertyName("caja")]
    public required string BoxId { get; set; }

    [JsonPropertyName("cantidad")]
    public required string Stock { get; set; }

    [JsonPropertyName("tipo")]
    public required string Kind { get; set; }
}