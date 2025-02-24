using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Queries.GetById;

public class GetByIdAddressQuery : IRequest<GetByIdAddressResponse>
{
    public Guid Id { get; set; }

    public class GetByIdAddressQueryHandler(IMapper mapper, IAddressRepository addressRepository, AddressBusinessRules addressBusinessRules) : IRequestHandler<GetByIdAddressQuery, GetByIdAddressResponse>
    {
        public async Task<GetByIdAddressResponse> Handle(GetByIdAddressQuery request, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            addressBusinessRules.AddressIsNotNull(address);

            GetByIdAddressResponse response = mapper.Map<GetByIdAddressResponse>(address);
            return response;
        }
    }
}