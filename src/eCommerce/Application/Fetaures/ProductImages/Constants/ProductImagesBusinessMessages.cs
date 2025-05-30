namespace Application.Fetaures.ProductImages.Constants;

public static class ProductImagesBusinessMessages
{
    public const string SectionName = "ProductImage";

    public const string ProductImageNotExists = "ProductImageNotExists";
    public const string ProductImageAlreadyMainImage = "There is already a main image for this product.";
    public const string ProductImageCannotBeDeletedIfOnlyOneExists = "ProductImageCannotBeDeletedIfOnlyOneExists";
    public const string ProductImageMainCannotBeDeletedIfNoOtherMain = "ProductImageMainCannotBeDeletedIfNoOtherMain";
    public const string ProductImageMainCannotBeDeleted = "ProductImageMainCannotBeDeleted";
}