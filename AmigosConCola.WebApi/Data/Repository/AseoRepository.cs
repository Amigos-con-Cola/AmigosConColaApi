using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AmigosConCola.WebApi.Data.Repository;

public sealed class AseoRepository : IAseosRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<AseoRepository> _logger;

    public AseoRepository(
        ApplicationDbContext db,
        ILogger<AseoRepository> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ErrorOr<Aseo>> Create(CreateAseoParams parameters)
    {
        var animal = await _db.Animals
            .Where(x => x.Id == parameters.IdAnimal)
            .FirstOrDefaultAsync();

        if (animal is null)
        {
            return Error.Validation(description: "The provided animal id is invalid");
        }

        var dto = new AseoDto
        {
            IdAnimal = parameters.IdAnimal,
            Tipo = parameters.Tipo,
            Fecha = parameters.Fecha
        };

        try
        {
            var result = await _db.Aseos.AddAsync(dto);
            await _db.SaveChangesAsync();

            return new Aseo
            {
                Id = result.Entity.Id,
                IdAnimal = result.Entity.IdAnimal,
                Tipo = result.Entity.Tipo,
                Fecha = result.Entity.Fecha
            };
        }
        catch (PostgresException ex)
        {
            _logger.LogError("There was an error while trying to create the aseo: {}", ex);
            return Error.Unexpected(description: "There was an error while trying to create the aseo");
        }
    }
}