using Application.Services.File;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Infrastructure.Services.File;

public class FileApiAdapter(HttpClient httpClient, IConfiguration configuration) : IFileService
{
    private readonly string fileApiUrl = configuration["FileApiUrl"] ??
        throw new InvalidOperationException("FileApiUrl can not be found in confifuration");

    public async Task<Stream> GetFileAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"{fileApiUrl}/api/File/Download/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<Guid> UploadFileAsync(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream();
        var streamContent = new StreamContent(fileStream);

        streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        {
            Name = "file",
            FileName = file.FileName
        };
        content.Add(streamContent);

        var response = await httpClient.PostAsync($"{fileApiUrl}/api/File/Upload", content);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var fileResult = JsonSerializer.Deserialize<FileUploadResponse>(jsonResponse, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true //Pascal case to camel case
            });

        return fileResult!.Id;

    }
}
