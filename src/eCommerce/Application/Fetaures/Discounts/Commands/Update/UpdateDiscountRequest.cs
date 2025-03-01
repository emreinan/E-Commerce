namespace Application.Fetaures.Discounts.Commands.Update;

public class UpdateDiscountRequest
{
    public decimal? Amount { get; set; }
    public decimal? Percentage { get; set; }
    public decimal MinOrderAmount { get; set; }
    public int UsageLimit { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
}