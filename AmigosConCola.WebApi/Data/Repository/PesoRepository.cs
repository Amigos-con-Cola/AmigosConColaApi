using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AmigosConCola.WebApi.Data.Repository;

public sealed class PesoRepository : IPesosRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<PesoRepository> _logger;
    private readonly IMapper _mapper;

    public PesoRepository(
        ApplicationDbContext db,
        ILogger<PesoRepository> logger,
        IMapper mapper)
    {
        _db = db;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Peso>> Create(CreatePesoParams parameters)
    {
        var animal = await _db.Animals
            .Where(x => x.Id == parameters.IdAnimal)
            .FirstOrDefaultAsync();
        if (animal is null)
        {
            return Error.Validation(description: "The provided animal id is invalid");
        }

        var dto = _mapper.Map<PesoDto>(parameters);

        try
        {
            var result = await _db.Pesos.AddAsync(dto);
            await _db.SaveChangesAsync();
            return _mapper.Map<Peso>(result.Entity);
        }
        catch (PostgresException ex)
        {
            _logger.LogError("There was an error while trying to create the peso: {}", ex);
            return Error.Unexpected(description: "There was an error while trying to create the peso");
        }
    }

    public async Task<IEnumerable<Peso>> FindAll(int idAnimal)
    {
        return await _db.Pesos
            .Where(x => x.IdAnimal == idAnimal)
            .OrderByDescending(x => x.Id)
            .Select(x => _mapper.Map<Peso>(x))
            .ToListAsync();
    }
}
