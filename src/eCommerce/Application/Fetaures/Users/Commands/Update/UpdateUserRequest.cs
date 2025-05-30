using Application.Fetaures.Users.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Users.Commands.Update;

public class UpdateUserRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public PersonalInfoDto? PersonalInfo { get; set; }
    public IFormFile? ProfileImage { get; set; }

}