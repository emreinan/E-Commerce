using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;
using Microsoft.AspNetCore.Http;
using Application.Services.File;

namespace Application.Fetaures.Stores.Commands.Create;

public class CreateStoreCommand : IRequest<CreatedStoreResponse>, ITransactionalRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Address { get; set; }
    public IFormFile? Logo { get; set; }

    public class CreateStoreCommandHandler(IMediator mediator,
                                           IMapper mapper,
                                           IStoreRepository storeRepository,
                                           StoreBusinessRules storeBusinessRules,
                                           IFileService fileService
        ) : IRequestHandler<CreateStoreCommand, CreatedStoreResponse>
    {
        public async Task<CreatedStoreResponse> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            await storeBusinessRules.StoreNameCannotBeDuplicated(request.Name, cancellationToken);
            await storeBusinessRules.StoreEmailCannotBeDuplicated(request.Email, cancellationToken);

            Store store = mapper.Map<Store>(request);

            if (request.Logo is not null)
            {
                var fileResponse = await fileService.UploadFileAsync(request.Logo);
                store.LogoUrl = fileResponse.Url;
            }

            await storeRepository.AddAsync(store, cancellationToken);

            await mediator.Publish(new SendStoreOwnerVerificationEvent
            {
                StoreId = store.Id,
                Email = store.Email,
                StoreName = store.Name
            }, cancellationToken);

            CreatedStoreResponse response = mapper.Map<CreatedStoreResponse>(store);
            return response;
        }
    }
}