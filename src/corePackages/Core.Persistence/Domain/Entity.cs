namespace Core.Persistence.Domain;

public abstract class Entity<TId>
{
    public TId Id { get; set; } = default!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
}