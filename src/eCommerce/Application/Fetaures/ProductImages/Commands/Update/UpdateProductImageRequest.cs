using Microsoft.AspNetCore.Http;

namespace Application.Fetaures.ProductImages.Commands.Update;

public class UpdateProductImageRequest
{
    public Guid ProductId { get; set; }
    public IFormFile File { get; set; } = default!;
    public bool IsMain { get; set; }
}
