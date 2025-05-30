
namespace Application.Fetaures.Baskets.Queries.GetById;

public class GetByIdBasketResponse 
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public Guid? DiscountId { get; set; }
}