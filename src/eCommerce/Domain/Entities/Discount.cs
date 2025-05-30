using Core.Persistence.Domain;
using Domain.Enums;

namespace Domain.Entities;

public class Discount : Entity<Guid>
{
    public string Code { get; set; } = default!;
    public decimal Value { get; set; } 
    public DiscountType Type { get; set; }
    public decimal MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public DateTime StartDate { get; set; } 
    public DateTime EndDate { get; set; } 
    public bool IsActive { get; set; } = true;

    public bool IsUsable =>
        IsActive && 
        EndDate > DateTime.UtcNow &&
        UsageLimit > 0;

}