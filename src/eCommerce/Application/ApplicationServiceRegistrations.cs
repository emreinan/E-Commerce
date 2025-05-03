using Application.Fetaures.Addresses.Rules;
using Application.Fetaures.Auth.Rules;
using Application.Services.Auth;
using Core.Application.Pipelines.Transaction;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistrations
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
        });

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<AddressBusinessRules>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(Application)));

        return services;
    }
}
