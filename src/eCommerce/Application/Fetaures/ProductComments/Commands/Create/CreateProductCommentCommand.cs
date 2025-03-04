using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Domain.Entities;
using MediatR;

namespace Application.Features.ProductComments.Commands.Create;

public class CreateProductCommentCommand : IRequest<CreatedProductCommentResponse>, ITransactionalRequest
{
    public  Guid ProductId { get; set; }
    public  Guid UserId { get; set; }
    public string Text { get; set; } = default!;
    public byte StarCount { get; set; }

    public class CreateProductCommentCommandHandler(IMapper mapper, IProductCommentRepository productCommentRepository) : IRequestHandler<CreateProductCommentCommand, CreatedProductCommentResponse>
    {
        public async Task<CreatedProductCommentResponse> Handle(CreateProductCommentCommand request, CancellationToken cancellationToken)
        {
            ProductComment productComment = mapper.Map<ProductComment>(request);

            await productCommentRepository.AddAsync(productComment);

            CreatedProductCommentResponse response = mapper.Map<CreatedProductCommentResponse>(productComment);
            return response;
        }
    }
}