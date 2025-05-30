using Application.Services.File;
using Application.Services.Mail;
using Infrastructure.Services.File;
using Infrastructure.Services.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureServiceRegistrations
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddTransient<IEmailService, SmtpEmailService>();
        services.AddTransient<IFileService, FileApiAdapter>();

        services.AddOptions<SmtpConfiguration>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection("SmtpConfiguration").Bind(settings);
            });

        services.AddHttpClient("FileApiClient", client =>
        {
            string apiUrl = configuration["FileApiUrl"] ?? throw new InvalidOperationException("FileApi URL is missing");
            client.BaseAddress = new Uri(apiUrl);
        });
        return services;
    }
}
