
namespace Application.Fetaures.Stores.Commands.Update;

public class UpdatedStoreResponse 
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Address { get; init; }
    public string? LogoUrl { get; init; }
    public bool IsActive { get; init; }
    public bool IsVerified { get; init; }
}
