using Application.Fetaures.Stores.Rules;
using Application.Services.Mail;
using Application.Services.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Web;

internal class SendStoreOwnerVerificationEvent : INotification
{
    public Guid StoreId { get; set; }
    public required string Email { get; set; }
    public required string StoreName { get; set; }

    internal class Handler(
        IEmailService emailService,
        IStoreRepository storeRepository,
        StoreBusinessRules storeBusinessRules,
        IConfiguration configuration) : INotificationHandler<SendStoreOwnerVerificationEvent>
    {
        public async Task Handle(SendStoreOwnerVerificationEvent notification, CancellationToken cancellationToken)
        {
            var store = await storeRepository.GetAsync(s => s.Id == notification.StoreId, cancellationToken: cancellationToken);
            storeBusinessRules.StoreShouldExistWhenSelected(store);

            var domain = configuration["Domain"];
            string encodedEmail = HttpUtility.UrlEncode(notification.Email);

            string link = $"{domain}/api/stores/verify?storeId={store!.Id}&email={encodedEmail}";

            string emailBody = $"""
                <h2>Store Verification Required</h2>
                <p>Hello, you’ve recently created a store: <strong>{notification.StoreName}</strong></p>
                <p>Please click the link below to verify your email:</p>
                <a href="{link}">Verify My Store</a>
                """;

            await emailService.SendEmailAsync(notification.Email, "Please verify your store", emailBody);
        }
    }
}
