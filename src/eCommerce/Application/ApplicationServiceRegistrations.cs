using Application.Fetaures.Addresses.Rules;
using Application.Fetaures.Auth.Rules;
using Application.Fetaures.BasketItems.Rules;
using Application.Fetaures.Baskets.Rules;
using Application.Fetaures.Categories.Rules;
using Application.Fetaures.Discounts.Rules;
using Application.Fetaures.OrderItems.Rules;
using Application.Fetaures.Orders.Rules;
using Application.Fetaures.ProductComments.Rules;
using Application.Fetaures.ProductImages.Rules;
using Application.Fetaures.Products.Rules;
using Application.Fetaures.Stores.Rules;
using Application.Fetaures.Users.Rules;
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
        services.AddScoped<BasketItemBusinessRules>();
        services.AddScoped<BasketItemBusinessRules>();
        services.AddScoped<BasketBusinessRules>();
        services.AddScoped<CategoryBusinessRules>();
        services.AddScoped<DiscountBusinessRules>();
        services.AddScoped<OrderItemBusinessRules>();
        services.AddScoped<OrderBusinessRules>();
        services.AddScoped<ProductBusinessRules>();
        services.AddScoped<ProductCommentBusinessRules>();
        services.AddScoped<ProductImageBusinessRules>();
        services.AddScoped<StoreBusinessRules>();
        services.AddScoped<UserBusinessRules>();


        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(Assembly.Load(nameof(Application)));

        return services;
    }
}
