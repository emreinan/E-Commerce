using Application.Fetaures.Auth.Commands.Refresh;
using Application.Fetaures.Auth.Commands.Login;
using Application.Fetaures.Auth.Commands.Register;
using Application.Fetaures.Auth.Commands.VerifyEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserForLoginDto dto)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            LoginCommand command = new() { Login = dto, IpAddress = ipAddress };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForRegisterDto dto)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            RegisterCommand command = new() { Register = dto, IpAddress = ipAddress };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefershTokenAsync([FromQuery] string refreshToken)
        {
            string ipAddress = HttpContext.Connection.RemoteIpAddress!.MapToIPv4().ToString();
            RefreshTokenCommand command = new() { Token = refreshToken, IpAddress = ipAddress };
            var response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("VerifyEmail")]
        public async Task<IActionResult> VerifyEmailAsync([FromQuery] VerifyEmailCommand command)
        {
            var response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
