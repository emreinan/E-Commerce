using Domain.Enums;

namespace Application.Fetaures.Discounts.Queries.GetList;

public class GetListDiscountListItemDto 
{
    public Guid Id { get; init; }
    public string Code { get; init; } = default!;
    public decimal Value { get; init; }
    public string Type { get; init; } = default!;
    public decimal? MinOrderAmount { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsActive { get; init; }
}