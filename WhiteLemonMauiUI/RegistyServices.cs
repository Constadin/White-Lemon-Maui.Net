using WhiteLemonMauiUI.Api.Interfaces;
using WhiteLemonMauiUI.Api.Services;
using WhiteLemonMauiUI.Users.Interfaces;
using WhiteLemonMauiUI.Users.Services;

namespace WhiteLemonMauiUI
{
    /// <summary>
    /// A static class for registering the UI services with dependency injection.
    /// Μια στατική κλάση για την καταχώρηση των υπηρεσιών UI με την έγχυση εξάρτησης.
    /// </summary>
    public static class RegistyServices
    {
        /// <summary>
        /// Registers the UI-related services such as API service and User service with the dependency injection container.
        /// Καταχωρεί τις υπηρεσίες σχετικές με το UI, όπως η υπηρεσία API και η υπηρεσία Χρήστη, στο δοχείο έγχυσης εξάρτησης.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> where services are registered.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with the added services.</returns>
        public static IServiceCollection RegisterUiServices(this IServiceCollection services)
        {
            // Registering services for dependency injection

            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDataSourceService, DataSourceService>();

            return services;
        }
    }
}
