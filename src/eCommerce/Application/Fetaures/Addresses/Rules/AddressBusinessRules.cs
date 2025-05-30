using Application.Services.Repositories;
using Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Application.Fetaures.Addresses.Constants;
using Microsoft.AspNetCore.Http;
using Application.Fetaures.Addresses.Commands.Update;

namespace Application.Fetaures.Addresses.Rules;
public class AddressBusinessRules(IAddressRepository addressRepository,
                                  IHttpContextAccessor httpContextAccessor) : BaseBusinessRules(httpContextAccessor)
{
    public void AddressIsNotNull(Address? address)
    {
        if (address is null)
            throw new BusinessException(AddressesBusinessMessages.AddressNotExists);
    }

    public async Task AddressIsExists(Guid addressId, CancellationToken cancellationToken)
    {
        var address = await addressRepository.AnyAsync(x => x.Id == addressId, cancellationToken: cancellationToken);
        if (!address)
            throw new BusinessException(AddressesBusinessMessages.AddressNotExists);
    }
    public async Task AddressTitleShouldBeUnique(Guid? userId, string? guestId, string addressTitle)
    {
        bool exists;

        if (userId != null)
        {
            exists = await addressRepository.AnyAsync(x => x.UserId == userId && x.AddressTitle == addressTitle);
        }
        else if (!string.IsNullOrEmpty(guestId))
        {
            exists = await addressRepository.AnyAsync(x => x.GuestId == guestId && x.AddressTitle == addressTitle);
        }
        else
        {
            throw new BusinessException("Kullanýcý veya guest bilgisi bulunamadý.");
        }

        if (exists)
            throw new BusinessException(AddressesBusinessMessages.AddressTitleMustBeUnique);
    }


    public async Task AddressTitleShouldBeUniqueForUpdate(Guid addressId, Guid? userId, string? guestId, string addressTitle)
    {
        Address? existing = null;
        if (userId.HasValue)
        {
            existing = await addressRepository.GetAsync(
                x => x.UserId == userId && x.AddressTitle == addressTitle && x.Id != addressId);
        }
        else if (!string.IsNullOrEmpty(guestId))
        {
            existing = await addressRepository.GetAsync(
                x => x.GuestId == guestId && x.AddressTitle == addressTitle && x.Id != addressId);
        }
        else
        {
            throw new BusinessException("Kullanýcý veya guest bilgisi eksik.");
        }

        if (existing != null)
            throw new BusinessException(AddressesBusinessMessages.AddressTitleMustBeUnique);
    }



    public async Task AddressPhoneShouldBeUniqueForGuest(string? guestId, string phoneNumber)
    {
        if (string.IsNullOrEmpty(guestId)) return;

        var exists = await addressRepository.AnyAsync(x =>
            x.GuestId == guestId && x.PhoneNumber == phoneNumber);

        if (exists)
            throw new BusinessException(AddressesBusinessMessages.PhoneNumberMustBeUniqueForGuest);
    }
    public void AddressMustBelongToUserOrGuest(Address address, Guid? userId, string? guestId)
    {
        if ((userId.HasValue && address.UserId != userId) ||
            (!userId.HasValue && address.GuestId != guestId))
        {
            throw new BusinessException(AddressesBusinessMessages.AddressDoesNotBelongToUser);
        }
    }
    public bool IsAddressModified(Address existing, UpdateAddressRequest request)
    {
        return existing.AddressTitle != request.AddressTitle ||
               existing.FullName != request.FullName ||
               existing.PhoneNumber != request.PhoneNumber ||
               existing.City != request.City ||
               existing.District != request.District ||
               existing.Street != request.Street ||
               existing.ZipCode != request.ZipCode ||
               existing.AddressDetail != request.AddressDetail;
    }
}