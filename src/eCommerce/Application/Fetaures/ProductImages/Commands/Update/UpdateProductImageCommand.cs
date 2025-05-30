using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Application.Fetaures.ProductImages.Rules;
using Core.Application.Pipelines.Transaction;
using Application.Services.File;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageCommand : IRequest<UpdatedProductImageResponse>, ITransactionalRequest
{
    public Guid Id { get; set; }
    public UpdateProductImageRequest Request { get; set; } = default!;

    public class UpdateProductImageCommandHandler(
        IProductImageRepository productImageRepository,
        IFileService fileService,
        ProductImageBusinessRules productImageBusinessRules,
        IMapper mapper)
        : IRequestHandler<UpdateProductImageCommand, UpdatedProductImageResponse>
    {
        public async Task<UpdatedProductImageResponse> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var productImage = await productImageRepository.GetAsync(
                predicate: pi => pi.Id == request.Id,
                cancellationToken: cancellationToken
            );

            productImageBusinessRules.ProductImageShouldExistWhenSelected(productImage);

            // Eðer yeni fotoðrafý main yapacaksak diðer main'leri kapat
            if (request.Request.IsMain)
            {
                await productImageBusinessRules.UnsetOtherMainImages(request.Request.ProductId, request.Id, cancellationToken);
            }

            var imageResponse = await fileService.UploadFileAsync(
                request.Request.File
            );

            productImage!.ImageUrl = imageResponse.Url;
            productImage.IsMain = request.Request.IsMain;

            await productImageRepository.UpdateAsync(productImage, cancellationToken: cancellationToken);

            UpdatedProductImageResponse response = mapper.Map<UpdatedProductImageResponse>(productImage);
            return response;
        }
    }
}

