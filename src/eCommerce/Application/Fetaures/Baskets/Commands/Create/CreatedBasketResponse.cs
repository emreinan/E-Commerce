
namespace Application.Fetaures.Baskets.Commands.Create;

public class CreatedBasketResponse 
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
}