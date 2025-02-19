namespace Domain.Enums;
public enum OrderStatus
{
    Pending,      // Sipariş oluşturuldu, ödeme bekleniyor
    Paid,         // Ödeme alındı, işleme alındı
    Shipped,      // Kargo firmasına teslim edildi
    Delivered,    // Müşteriye teslim edildi
    Canceled,     // Sipariş iptal edildi
    Returned      // Müşteri iade etti
}
