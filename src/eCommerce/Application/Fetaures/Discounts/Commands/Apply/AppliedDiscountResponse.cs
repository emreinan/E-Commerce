using Domain.Enums;

namespace Application.Fetaures.Discounts.Commands.Apply;

public class AppliedDiscountResponse
{
    public required string Code { get; set; }
    public string Type { get; set; } = default!;
    public decimal Value { get; set; }              
    public decimal TotalDiscountValue { get; set; }      // Siparişte uygulanan değer
    public decimal NewTotal { get; set; }           // İndirim sonrası sipariş tutarı
}
