using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WhiteLemon.Application.Interfaces;
using WhiteLemon.Application.Services;
using WhiteLemon.Domain.Entities;

namespace WhiteLemon.Application
{
    public static class RegistersDependencyInjection
    {
        /// <summary>
        /// Registers application services for dependency injection.
        /// Καταχωρεί τις υπηρεσίες της εφαρμογής για το dependency injection.
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IUserService, UserService>();


            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            return services;
        }
    }
}
