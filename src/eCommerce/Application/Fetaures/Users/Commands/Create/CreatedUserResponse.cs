namespace Application.Fetaures.Users.Commands.Create;

public class CreatedUserResponse 
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;   
    public bool IsActive { get; set; }

}
