using AmigosConCola.WebApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmigosConCola.WebApi.Data.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<AnimalEntity> Animals { get; set; } = null!;
    public DbSet<VacunacionEntity> Vacunaciones { get; set; } = null!;
    public DbSet<DesparasitacionEntity> Desparasitaciones { get; set; } = null!;
    public DbSet<AseoEntity> Aseos { get; set; } = null!;
    public DbSet<PesoEntity> Pesos { get; set; } = null!;
    public DbSet<InventoryItemEntity> Inventory { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnimalEntity>()
            .HasMany(x => x.Vacunaciones)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);

        modelBuilder.Entity<AnimalEntity>()
            .HasMany(x => x.Desparasitaciones)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);

        modelBuilder.Entity<AnimalEntity>()
            .HasMany(x => x.Aseos)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);

        modelBuilder.Entity<AnimalEntity>()
            .HasMany(x => x.Pesos)
            .WithOne(x => x.Animal)
            .HasForeignKey(x => x.IdAnimal)
            .HasPrincipalKey(x => x.Id);
    }
}