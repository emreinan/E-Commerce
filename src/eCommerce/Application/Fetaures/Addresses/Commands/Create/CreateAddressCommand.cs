using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Commands.Create;

public class CreateAddressCommand : IRequest<CreatedAddressResponse>, ITransactionalRequest
{
    public Guid? UserId { get; set; }
    public string? GuestId { get; set; }
    public string AddressTitle { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string? ZipCode { get; set; }
    public string AddressDetail { get; set; } = default!;

    public class CreateAddressCommandHandler(IMapper mapper, IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<CreateAddressCommand, CreatedAddressResponse>
    {
        public async Task<CreatedAddressResponse> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var guestId = addressBusinessRules.IfUserIdIsNullGetOrCreateGuestId(request.UserId);
            request.GuestId = guestId;
            await addressBusinessRules.AddressTitleSholdBeUnique(request.UserId, request.GuestId ,request.AddressTitle);

            Address address = mapper.Map<Address>(request);

            await addressRepository.AddAsync(address);

            CreatedAddressResponse response = mapper.Map<CreatedAddressResponse>(address);
            return response;
        }
    }
}