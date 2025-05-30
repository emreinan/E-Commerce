
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Stores.Commands.Update;

public record UpdateStoreRequest(
   string Name,
   string Description,
   string Email,
   string PhoneNumber,
   string Address,
   IFormFile? Logo,
   bool IsActive,
   bool IsVerified
);
