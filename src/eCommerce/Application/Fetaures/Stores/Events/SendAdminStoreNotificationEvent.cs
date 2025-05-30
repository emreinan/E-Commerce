using Application.Services.Mail;
using MediatR;
using Microsoft.Extensions.Configuration;

internal class SendAdminStoreNotificationEvent : INotification
{
    public Guid StoreId { get; set; }
    public required string StoreName { get; set; }
    public required string OwnerEmail { get; set; }

    internal class Handler(
        IEmailService emailService,
        IConfiguration configuration) : INotificationHandler<SendAdminStoreNotificationEvent>
    {
        public async Task Handle(SendAdminStoreNotificationEvent notification, CancellationToken cancellationToken)
        {
            var adminEmail = configuration["Admin:Email"];

            string emailBody = $"""
                <h2>Store Verified</h2>
                <p>The store <strong>{notification.StoreName}</strong> (Owner: {notification.OwnerEmail}) has completed email verification.</p>
                <p>Please log in to the admin panel to activate this store.</p>
                <p>
                    <a href="https://admin.mydomain.com/stores/{notification.StoreId}">Go to Store Activation Page</a>
                </p>
                """;

            await emailService.SendEmailAsync(adminEmail!, $"Store '{notification.StoreName}' is ready for activation", emailBody);
        }
    }
}
