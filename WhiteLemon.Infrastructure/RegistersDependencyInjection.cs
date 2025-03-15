using Microsoft.Extensions.DependencyInjection;
using WhiteLemon.Domain.Interfaces;
using WhiteLemon.Infrastructure.Repositories;

namespace WhiteLemon.Infrastructure
{
    /// <summary>
    /// Contains methods to register infrastructure services for dependency injection.
    /// Περιέχει μεθόδους για την καταχώριση υπηρεσιών υποδομής για την εξάρτηση των υπηρεσιών.
    /// </summary>
    public static class RegistersDependencyInjection
    {
        /// <summary>
        /// Registers the infrastructure services, such as repositories, in the dependency injection container.
        /// Καταχωρεί τις υπηρεσίες υποδομής, όπως τα αποθετήρια, στο δοχείο εξάρτησης των υπηρεσιών.
        /// </summary>
        /// <param name="services">The service collection to which services are added. 
        /// Η συλλογή υπηρεσιών στην οποία προστίθενται οι υπηρεσίες.</param>
        /// <returns>The updated service collection with the registered services. 
        /// Η ενημερωμένη συλλογή υπηρεσιών με τις καταχωρημένες υπηρεσίες.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
