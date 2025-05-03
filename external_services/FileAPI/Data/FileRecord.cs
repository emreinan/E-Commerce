namespace FileAPI.Data;

public class FileRecord
{

   public Guid Id { get; set; }
    public string OriginalFileName { get; set; } = default!;
    public string FileExtension { get; set; } = default!;
    public string FileName { get; set; } = default!;
    public string FilePath { get; set; } = default!;
    public DateTime UploadedAt { get; set; }
}
