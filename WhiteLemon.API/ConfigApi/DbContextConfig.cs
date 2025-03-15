using Microsoft.EntityFrameworkCore;
using WhiteLemon.Infrastructure.Data;

namespace WhiteLemon.API.ConfigApi
{
    public static class DbContextConfig
    {
        public static void AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
        {
            /// <summary>
            /// Configures the application's database context to use SQL Server with a connection string.
            /// Διαμορφώνει το DbContext της εφαρμογής να χρησιμοποιεί SQL Server με μια σύνδεση βάσης δεδομένων.
            /// </summary>

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WhiteLemonDBConnection"));
            });
        }
    }
}
