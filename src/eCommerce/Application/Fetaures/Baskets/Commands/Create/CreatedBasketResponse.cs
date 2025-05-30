
namespace Application.Fetaures.Baskets.Commands.Create;

public record CreatedBasketResponse(Guid Id, Guid? UserId, string? GuestId);
