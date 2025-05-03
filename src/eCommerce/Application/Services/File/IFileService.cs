using Microsoft.AspNetCore.Http;

namespace Application.Services.File;

public class FileUploadResponse
{
    public Guid Id { get; set; }
}
public interface IFileService
{
    Task<Guid> UploadFileAsync(IFormFile file);
    Task<Stream> GetFileAsync(Guid id);
}
