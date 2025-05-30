using Microsoft.AspNetCore.Http;

namespace Application.Services.File;

public class FileUploadResponse
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
}

public interface IFileService
{
    Task<FileUploadResponse> UploadFileAsync(IFormFile file);
    Task<Stream> GetFileAsync(Guid id);
}
