namespace Application.Fetaures.Users.Commands.Create;

public class CreatedUserResponse 
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;   
    public bool IsActive { get; init; }

}
