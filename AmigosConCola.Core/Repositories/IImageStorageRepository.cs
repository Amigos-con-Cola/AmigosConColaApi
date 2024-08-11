namespace AmigosConCola.Core.Repositories;

public interface IImageStorageRepository
{
    /// <summary>
    ///     Store an image from the provided stream with the provided filename.
    /// </summary>
    /// <param name="imageStream">The stream containing the image data.</param>
    /// <param name="fileName">The name of the image.</param>
    /// <returns>A Uri of where the image is located.</returns>
    Task<Uri> StoreImage(Stream imageStream, string fileName);
}