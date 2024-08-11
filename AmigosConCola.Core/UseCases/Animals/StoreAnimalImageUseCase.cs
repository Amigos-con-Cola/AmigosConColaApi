using AmigosConCola.Core.Repositories;
using ErrorOr;

namespace AmigosConCola.Core.UseCases.Animals;

public class StoreAnimalImageUseCase
{
    private readonly IImageStorageRepository _images;

    public StoreAnimalImageUseCase(IImageStorageRepository images)
    {
        _images = images;
    }

    public async Task<ErrorOr<Uri>> Invoke(Stream stream, string fileName)
    {
        return await _images.StoreImage(stream, fileName);
    }
}