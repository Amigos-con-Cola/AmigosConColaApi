using AmigosConCola.Core.Repositories;

namespace AmigosConCola.WebApi.Data.Repository;

public class ImageStorageRepository : IImageStorageRepository
{
    private const string ImagesDirectory = "Images";
    private readonly IWebHostEnvironment _environment;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ImageStorageRepository(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
    {
        _environment = environment;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Uri> StoreImage(Stream imageStream, string fileName)
    {
        var filePath = Path.Combine(_environment.ContentRootPath, ImagesDirectory, fileName);

        await using var fileStream = new FileStream(filePath, FileMode.Create);
        await imageStream.CopyToAsync(fileStream);

        var request = _httpContextAccessor.HttpContext?.Request;
        var imageUrl = $"{request?.Scheme}://{request?.Host}/images/{fileName}";

        return new Uri(imageUrl);
    }
}