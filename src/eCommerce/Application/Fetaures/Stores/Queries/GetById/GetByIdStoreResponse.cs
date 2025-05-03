
namespace Application.Fetaures.Stores.Queries.GetById;

public class GetByIdStoreResponse 
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
}