using AmigosConCola.WebApi.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace AmigosConCola.WebApi.Data.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<AnimalDto> Animals { get; set; } = null!;
}