using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.Stores.Rules;
using Core.Application.Pipelines.Transaction;
using Application.Services.File;

namespace Application.Fetaures.Stores.Commands.Update;

public class UpdateStoreCommand : IRequest<UpdatedStoreResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required UpdateStoreRequest Request { get; set; } 

    public class UpdateStoreCommandHandler(IMapper mapper,
                                           IStoreRepository storeRepository,
                                           IFileService fileService,
                                           StoreBusinessRules storeBusinessRules) : IRequestHandler<UpdateStoreCommand, UpdatedStoreResponse>
    {
        public async Task<UpdatedStoreResponse> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
        {
            Store? store = await storeRepository.GetAsync(
                predicate: s => s.Id == command.Id,
                cancellationToken: cancellationToken);

            storeBusinessRules.StoreShouldExistWhenSelected(store);

            if (command.Request.Logo is not null)
            {
                var fileResponse = await fileService.UploadFileAsync(command.Request.Logo);
                store!.LogoUrl = fileResponse.Url;
            }

            mapper.Map(command.Request, store);

            await storeRepository.UpdateAsync(store!, cancellationToken: cancellationToken);

            UpdatedStoreResponse response = mapper.Map<UpdatedStoreResponse>(store);
            return response;
        }
    }
}