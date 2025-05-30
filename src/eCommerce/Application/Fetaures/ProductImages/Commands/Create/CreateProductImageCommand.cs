using Application.Fetaures.ProductImages.Rules;
using Application.Services.File;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Application.Fetaures.ProductImages.Commands.Create;

public record CreateProductImageCommand(
    Guid ProductId,
    [property: SwaggerSchema(Description = 
    "Yüklenecek fotoðraflar. Sýrasý önemlidir: ilk dosya için 0, ikinci dosya için 1 vs.")]
    List<IFormFile> Files,

    [property: SwaggerSchema(Description = 
    "Ana fotoðraf olarak seçilecek dosyanýn sýrasý (0, 1, 2). " +
    "Tek dosya yüklenirse otomatik ana fotoðraf olur.")]
    int MainImageIndex
) : IRequest<List<CreatedProductImageResponse>>
{
    public class Handler(
        IProductImageRepository repository,
        IFileService fileService,
        IMapper mapper,
        ProductImageBusinessRules rules
    ) : IRequestHandler<CreateProductImageCommand, List<CreatedProductImageResponse>>
    {
        public async Task<List<CreatedProductImageResponse>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            bool anyNewMain = request.Files.Count > 0 && request.MainImageIndex >= 0;
            await rules.EnsureSingleMainImageAllowed(request.ProductId, anyNewMain, cancellationToken);

            var responses = new List<CreatedProductImageResponse>();

            int mainImageIndex = request.Files.Count == 1 ? 0 : request.MainImageIndex;
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var uploaded = await fileService.UploadFileAsync(file);

                var image = new ProductImage
                {
                    ProductId = request.ProductId,
                    ImageUrl = uploaded.Url,
                    IsMain = i == mainImageIndex,
                };

                await repository.AddAsync(image, cancellationToken);
                responses.Add(mapper.Map<CreatedProductImageResponse>(image));
            }

            return responses;
        }
    }
}
