
namespace Application.Fetaures.Stores.Commands.Update;

public class UpdatedStoreResponse 
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Address { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
}
