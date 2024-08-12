using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation.Requests;

public class CreateInventoryItemRequest
{
    [JsonPropertyName("nombre")]
    public required string Name { get; set; }

    [JsonPropertyName("ingrediente_principal")]
    public required string MainIngredient { get; set; }

    [JsonPropertyName("formato")]
    public string? Format { get; set; }

    [JsonPropertyName("volumen")]
    public required int Volume { get; set; }

    [JsonPropertyName("via")]
    public required string Via { get; set; }

    [DataType(DataType.Date)]
    [JsonPropertyName("fecha_vencimiento")]
    public required DateOnly ExpirationDate { get; set; }

    [JsonPropertyName("laboratorio")]
    public string? Laboratory { get; set; }

    [JsonPropertyName("origen")]
    public string? Origin { get; set; }

    [JsonPropertyName("estado")]
    public required string Status { get; set; }

    [DataType(DataType.Date)]
    [JsonPropertyName("fecha_registro")]
    public required DateOnly EntryDate { get; set; }

    [JsonPropertyName("ubicacion")]
    public required string Location { get; set; }

    [JsonPropertyName("tipo")]
    public required string Kind { get; set; }
}