using Application.Fetaures.Addresses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Fetaures.Addresses.Commands.Delete;

public class DeleteAddressCommand : IRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public class DeleteAddressCommandHandler(IAddressRepository addressRepository,
                                     AddressBusinessRules addressBusinessRules) : IRequestHandler<DeleteAddressCommand>
    {
        public async Task Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            Address? address = await addressRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            addressBusinessRules.AddressIsNotNull(address);

            await addressRepository.DeleteAsync(address!, cancellationToken: cancellationToken);
        }
    }
}