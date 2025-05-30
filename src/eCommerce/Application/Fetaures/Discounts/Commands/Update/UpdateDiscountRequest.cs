using Domain.Enums;

namespace Application.Fetaures.Discounts.Commands.Update;

public record UpdateDiscountRequest(
    decimal Value,
    DiscountType Type,
    decimal MinOrderAmount,
    int UsageLimit,
    DateTime EndDate,
    bool IsActive
);
