using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Commands.Update;

public class UpdateAddressCommand : IRequest<UpdatedAddressResponse>, ITransactionalRequest
{
    public Guid Id { get; set; } = default!;    
    public UpdateAddressRequest Request { get; set; } = default!;

    public class UpdateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<UpdateAddressCommand, UpdatedAddressResponse>
    {
        public async Task<UpdatedAddressResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            addressBusinessRules.AddressIsNotNull(address);

            var isUser = request.Request.UserId.HasValue;
            var guestId = isUser ? null : addressBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.Request.UserId);

            address!.UserId = isUser ? request.Request.UserId : null;
            address.GuestId = isUser ? null : guestId;

            // Adres bu kullanýcýya mý ait?
            addressBusinessRules.AddressMustBelongToUserOrGuest(address!, request.Request.UserId, guestId);

            // Unique kontrol
            await addressBusinessRules.AddressTitleShouldBeUniqueForUpdate(
                request.Id,
                isUser ? request.Request.UserId : null,
                !isUser ? guestId : null,
                request.Request.AddressTitle);


            // Eðer hiçbir alan deðiþmemiþse, iþlem yapma.  
            if (!addressBusinessRules.IsAddressModified(address!, request.Request))
            {
                return mapper.Map<UpdatedAddressResponse>(address); // ayný adresi döneriz  
            }

            address = mapper.Map(request.Request, address);

            await addressRepository.UpdateAsync(address!, cancellationToken);

            UpdatedAddressResponse response = mapper.Map<UpdatedAddressResponse>(address);
            return response;
        }
    }
}
