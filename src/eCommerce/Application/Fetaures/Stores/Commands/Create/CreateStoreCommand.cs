using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;

namespace Application.Fetaures.Stores.Commands.Create;

public class CreateStoreCommand : IRequest<CreatedStoreResponse>, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Address { get; set; }
    public string? LogoUrl { get; set; }
    public required bool IsActive { get; set; }
    public required bool IsVerified { get; set; }

    public class CreateStoreCommandHandler(IMapper mapper, IStoreRepository storeRepository) : IRequestHandler<CreateStoreCommand, CreatedStoreResponse>
    {
        public async Task<CreatedStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            Store store = mapper.Map<Store>(request);

            await storeRepository.AddAsync(store);

            CreatedStoreResponse response = mapper.Map<CreatedStoreResponse>(store);
            return response;
        }
    }
}