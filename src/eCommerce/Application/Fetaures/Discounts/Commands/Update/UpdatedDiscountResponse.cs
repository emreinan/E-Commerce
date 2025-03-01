

namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdatedDiscountResponse 
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public decimal Amount { get; set; }
    public decimal? Percentage { get; set; }
    public decimal? MinOrderAmount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}
