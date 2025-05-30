using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Commands.Create;
public sealed record CreateAddressCommand(
     Guid? UserId,
     string AddressTitle,
     string? FullName,
     string PhoneNumber,
     string Street,
     string District,
     string City,
     string? ZipCode,
     string AddressDetail
) : IRequest<CreatedAddressResponse>, ITransactionalRequest
{
    public class CreateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<CreateAddressCommand, CreatedAddressResponse>
    {
        public async Task<CreatedAddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var guestId = addressBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UserId);

            string? fullName = request.FullName;
            if (request.UserId.HasValue)
            {
                await addressBusinessRules.AddressTitleShouldBeUnique(request.UserId, null, request.AddressTitle);
                var name = addressBusinessRules.GetClaimName();
                var surname = addressBusinessRules.GetClaimSurname();
                fullName = $"{name} {surname}";
            }
            else
            {
                await addressBusinessRules.AddressTitleShouldBeUnique(null, guestId, request.AddressTitle);
            }

            var address = mapper.Map<Address>(request);
            address.GuestId = request.UserId.HasValue ? null : guestId;
            address.FullName = fullName!;

            await addressRepository.AddAsync(address, cancellationToken);

            var response = mapper.Map<CreatedAddressResponse>(address);
            return response;
        }
    }
}
