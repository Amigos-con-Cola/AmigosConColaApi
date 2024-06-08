using System.Text.Json.Serialization;

namespace AmigosConCola.WebApi.Presentation;

public class PaginatedDataResponse<T>
    where T : class
{
    [JsonPropertyName("data")]
    public IEnumerable<T> Data { get; set; } = null!;

    [JsonPropertyName("nextPage")]
    public int NextPage { get; set; }

    [JsonPropertyName("totalItems")]
    public int TotalItems { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
}