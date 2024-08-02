namespace AmigosConCola.Core.Repositories;

public sealed class PaginationParams(int page = 1, int perPage = 10)
{
    public int Page { get; set; } = page;
    public int PerPage { get; set; } = perPage;
}