namespace Application.Fetaures.Auth.Commands.Register;

public class RegisteredResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
