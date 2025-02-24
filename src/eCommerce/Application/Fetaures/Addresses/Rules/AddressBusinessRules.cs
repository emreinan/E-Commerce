using Application.Services.Repositories;
using Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Application.Fetaures.Addresses.Constants;
using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.Addresses.Rules;

public class AddressBusinessRules(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor) : BaseBusinessRules
{

    public void AddressIsNotNull(Address? address)
    {
        if (address is null)
            throw new BusinessException(AddressesBusinessMessages.AddressNotExists);
    }

    public async Task AddressIsExists(Guid addressId)
    {
        var address = await addressRepository.AnyAsync(x => x.Id == addressId);
        if (!address)
            throw new BusinessException(AddressesBusinessMessages.AddressNotExists);
    }
    public async Task AddressTitleSholdBeUnique(Guid? userId, string? guestId, string addressTitle)
    {
        var address = await addressRepository.GetAsync(x =>
            (x.UserId == userId || x.GuestId == guestId) && x.AddressTitle == addressTitle);

        if (address is not null)
            throw new BusinessException(AddressesBusinessMessages.AddressTitleMustBeUnique);
    }

    public string IfUserIdIsNullGetOrCreateGuestId(Guid? userId)
    {
        if (userId.HasValue)
            return string.Empty; // Kullanýcý giriþ yapmýþsa guestId kullanma.

        var guestId = httpContextAccessor.HttpContext?.Request.Cookies["GuestId"];

        if (string.IsNullOrEmpty(guestId))
        {
            guestId = Guid.NewGuid().ToString();
            httpContextAccessor.HttpContext?.Response.Cookies.Append("GuestId", guestId, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });
        }

        return guestId;
    }

}