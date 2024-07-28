using AmigosConCola.Core.Models;
using AmigosConCola.Core.Repositories;
using AmigosConCola.WebApi.Data.Database;
using AmigosConCola.WebApi.Data.Dto;
using AutoMapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace AmigosConCola.WebApi.Data.Repository;

public sealed class AseoRepository : IAseosRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<AseoRepository> _logger;
    private readonly IMapper _mapper;

    public AseoRepository(
        ApplicationDbContext db,
        ILogger<AseoRepository> logger,
        IMapper mapper)
    {
        _db = db;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ErrorOr<Aseo>> Create(CreateAseoParams parameters)
    {
        var animal = await _db.Animals
            .Where(x => x.Id == parameters.IdAnimal)
            .FirstOrDefaultAsync();

        if (animal is null)
            return Error.NotFound(description: "The provided animal id is invalid");

        var dto = _mapper.Map<AseoDto>(parameters);

        try
        {
            var result = await _db.Aseos.AddAsync(dto);
            await _db.SaveChangesAsync();
            return _mapper.Map<Aseo>(result.Entity);
        }
        catch (PostgresException ex)
        {
            _logger.LogError("There was an error while trying to create the aseo: {}", ex);
            return Error.Unexpected(description: "There was an error while trying to create the aseo");
        }
    }

    public async Task<IEnumerable<Aseo>> FindAll(int idAnimal)
    {
        return await _db.Aseos
            .Where(x => x.IdAnimal == idAnimal)
            .OrderByDescending(x => x.Id)
            .Select(x => _mapper.Map<Aseo>(x))
            .ToListAsync();
    }
}