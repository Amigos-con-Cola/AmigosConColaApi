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
    public DbSet<VacunacionDto> Vacunaciones { get; set; } = null!;
    public DbSet<DesparasitacionDto> Desparasitaciones { get; set; } = null!;
    public DbSet<AseoDto> Aseos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalDto>()
            .HasMany(x => x.Vacunaciones)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);

        modelBuilder.Entity<AnimalDto>()
            .HasMany(x => x.Desparasitaciones)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);

        modelBuilder.Entity<AnimalDto>()
            .HasMany(x => x.Aseos)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);
    }
}