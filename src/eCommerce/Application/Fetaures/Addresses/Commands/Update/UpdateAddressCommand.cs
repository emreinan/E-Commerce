using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Commands.Update;

public class UpdateAddressCommand : IRequest<UpdatedAddressResponse> , ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateAddressRequest UpdateAddressRequest { get; set; } = default!;

    public class UpdateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<UpdateAddressCommand, UpdatedAddressResponse>
    {
        public async Task<UpdatedAddressResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            addressBusinessRules.AddressIsNotNull(address);

            var guestId = addressBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UpdateAddressRequest.UserId);
            request.UpdateAddressRequest.GuestId = guestId;

            await addressBusinessRules.AddressTitleSholdBeUnique(request.UpdateAddressRequest.UserId, request.UpdateAddressRequest.GuestId, request.UpdateAddressRequest.AddressTitle);

            address = mapper.Map(request.UpdateAddressRequest, address);

            await addressRepository.UpdateAsync(address!, cancellationToken: cancellationToken);

            UpdatedAddressResponse response = mapper.Map<UpdatedAddressResponse>(address);
            return response;
        }
    }
}