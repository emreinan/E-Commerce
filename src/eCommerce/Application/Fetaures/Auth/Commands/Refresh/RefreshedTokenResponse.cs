namespace Application.Features.Auth.Commands.Refresh;

public partial class RefreshTokenCommand
{
    public class RefreshedTokenResponse
    {
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
