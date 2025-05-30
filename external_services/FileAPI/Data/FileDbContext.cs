using Microsoft.EntityFrameworkCore;

namespace FileAPI.Data;

public class FileDbContext : DbContext
{
    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
    {
    }

    public DbSet<FileRecord> FileRecords { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileRecord>().HasKey(x => x.Id);
        modelBuilder.Entity<FileRecord>().Property(x => x.OriginalFileName).IsRequired();
        modelBuilder.Entity<FileRecord>().Property(x => x.FileExtension).IsRequired();
        modelBuilder.Entity<FileRecord>().Property(x => x.FileName).IsRequired();
        modelBuilder.Entity<FileRecord>().Property(x => x.FilePath).IsRequired();
        modelBuilder.Entity<FileRecord>().Property(x => x.UploadedAt).IsRequired();
    }
}
