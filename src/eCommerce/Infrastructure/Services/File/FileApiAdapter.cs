using Application.Services.File;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Infrastructure.Services.File;

public class FileApiAdapter(IHttpClientFactory httpClientFactory) : IFileService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient("FileApiClient");

    public async Task<Stream> GetFileAsync(Guid id)
    {
        var response = await httpClient.GetAsync($"/api/File/Download/{id}");
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<FileUploadResponse> UploadFileAsync(IFormFile file)
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

        var response = await httpClient.PostAsync($"/api/File/Upload", content);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var fileResult = JsonSerializer.Deserialize<FileUploadResponse>(jsonResponse, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true //Pascal case to camel case
            });

        return fileResult!;

    }
}
