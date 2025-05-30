using Application.Services.Repositories;
using Core.Application.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Seeding;

public static class DataSeeder
{
    public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var roleRepository = scope.ServiceProvider.GetRequiredService<IRoleRepository>();

        HashingHelper.CreatePasswordHash("1234", out byte[] passwordHash, out byte[] passwordSalt);

        if (!await roleRepository.AnyAsync())
        {
            await roleRepository.AddRangeAsync(
        [
            new() { Name = "Admin" },
            new() { Name = "User" }
        ]);
        }

        if (!await userRepository.AnyAsync())
        {
            var adminRole = await roleRepository.GetAsync(predicate: r => r.Name == "Admin");
            var userRole = await roleRepository.GetAsync(predicate: r => r.Name == "User");

            await userRepository.AddRangeAsync(
        [
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@mail.com",
                PhoneNumber = "1234567890",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                UserRoles =
                [
                    new() 
                    {
                        RoleId = adminRole!.Id,
                        CreatedDate = DateTime.UtcNow,
                    }
                ],
            },
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "User",
                LastName = "User",
                Email = "user@mail.com",
                PhoneNumber = "0987654321",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                IsActive = true,
                UserRoles =
                [
                    new() 
                    {
                        RoleId = userRole!.Id,
                        CreatedDate = DateTime.UtcNow,
                    }
                ],
            }
        ]);
        }

    }

}
