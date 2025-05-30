namespace Application.Fetaures.Discounts.Constants;

public static class DiscountsBusinessMessages
{
    public const string SectionName = "Discount";

    public const string DiscountNotExists = "DiscountNotExists";
    public const string DiscountCodeShouldBeUnique = "DiscountCodeShouldBeUnique";
    public const string DiscountNotUsable = "DiscountNotUsable";    
    public const string DiscountAlreadyActive = "DiscountAlreadyActive";
    public const string DiscountAlreadyInactive = "DiscountAlreadyInactive";
    public const string DiscountNotFound = "DiscountNotFound";
    public const string DiscountCodeAlreadyUsed = "DiscountCodeAlreadyUsed";
    public const string DiscountCodeExpired = "DiscountCodeExpired";
    public const string DiscountCodeHasReachedUsageLimir = "This discount code has reached its usage limit.";
}