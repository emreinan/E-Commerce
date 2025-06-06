﻿using FileAPI.Data;
using FileAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;

namespace FileAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly string _fileStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
    private readonly FileDbContext _context;

    public FileController(FileDbContext context)
    {
        _context = context;
        if (!Directory.Exists(_fileStoragePath))
        {
            Directory.CreateDirectory(_fileStoragePath);
        }
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadFile([FromForm] FileModel model)
    {
        var file = model.File;
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file.");
        }

        var fileId = Guid.NewGuid();
        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{fileId}{fileExtension}";
        var filePath = Path.Combine(_fileStoragePath, fileName);

        var fileRecord = new FileRecord
        {
            Id = fileId,
            OriginalFileName = file.FileName,
            FileExtension = fileExtension,
            FileName = fileName,
            FilePath = filePath,
            UploadedAt = DateTime.UtcNow,
        };

        using var fileStream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(fileStream);

        _context.FileRecords.Add(fileRecord);
        await _context.SaveChangesAsync();

        var fileUrl = Url.Action(nameof(GetFileByUrl), "File", new { id = fileRecord.Id }, Request.Scheme);

        return Ok(new UploadFileResponse(fileRecord.Id, fileUrl!));
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadFile([FromQuery] Guid id)
    {
        var fileRecord = await _context.FileRecords.FindAsync(id);

        if (fileRecord == null || !System.IO.File.Exists(fileRecord.FilePath))
        {
            return NotFound();
        }

        var fileStream = new FileStream(fileRecord.FilePath, FileMode.Open, FileAccess.Read);
        var contentType = GetContentType(fileRecord.FileName);

        return File(fileStream, contentType, fileRecord.OriginalFileName);
    }

    [HttpGet("getByUrl")]
    [AllowAnonymous]
    public async Task<IActionResult> GetFileByUrl(Guid id)
    {
        var fileRecord = await _context.FileRecords.FindAsync(id);

        if (fileRecord == null || !System.IO.File.Exists(fileRecord.FilePath))
        {
            return NotFound();
        }

        var contentType = GetContentType(fileRecord.FileName);
        var fileStream = new FileStream(fileRecord.FilePath, FileMode.Open, FileAccess.Read);

        Response.Headers[HeaderNames.ContentDisposition] = new ContentDispositionHeaderValue("inline").ToString();
        return File(fileStream, contentType);
    }
    private static string GetContentType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        return provider.TryGetContentType(fileName, out string? contentType)
            ? contentType
            : "application/octet-stream";
    }
}
