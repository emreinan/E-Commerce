using Application.Services.Mail;
using Infrastructure.Services.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, SmtpEmailService>();
        services.AddOptions<SmtpConfiguration>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("SmtpConfiguration").Bind(settings);
            });
        return services;
    }
}
