namespace WhiteLemon.API.ConfigApi
{
    public static class BuilderServicesConfig
    {
        public static void AddBuilderDependencies(this WebApplicationBuilder builder)
        {
            /// <summary>
            /// Adds services to the container.
            /// Προσθέτει υπηρεσίες στο κοντέινερ.
            /// </summary>
            builder.Services.AddOpenApiServices();

            /// <summary>
            /// Configures CORS (Cross-Origin Resource Sharing) policies for the application.
            /// Διαμορφώνει τις πολιτικές CORS (Cross-Origin Resource Sharing) για την εφαρμογή.
            /// </summary>
            builder.Services.AddCorsConfig();


            /// <summary>
            /// Adds controllers to the container, which handles HTTP requests and responses.
            /// Προσθέτει controllers στο κοντέινερ, που χειρίζονται τα HTTP αιτήματα και τις απαντήσεις.
            /// </summary>
            builder.Services.AddControllers();


            /// <summary>
            /// Configures the application's database context to use SQL Server with a connection string.
            /// Διαμορφώνει το DbContext της εφαρμογής να χρησιμοποιεί SQL Server με μια σύνδεση βάσης δεδομένων.
            /// </summary>
            builder.Services.AddDbContextConfig(builder.Configuration);

        }
    }
}
