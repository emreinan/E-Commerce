using Domain.Enums;

namespace Application.Fetaures.Discounts.Queries.GetByCode;

public class GetByCodeDiscountResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = null!;
    public decimal Value { get; init; }
    public string Type { get; init; } = default!;
    public decimal MinOrderAmount { get; init; }
    public int UsageLimit { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
    public bool IsUsable { get; init; } 
}
