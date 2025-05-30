namespace Application.Fetaures.Auth.Commands.Refresh;


public class RefreshedTokenResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}

