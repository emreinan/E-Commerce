
namespace Application.Fetaures.Baskets.Queries.GetList;

public class GetListBasketListItemDto 
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public Guid? DiscountId { get; set; }
}