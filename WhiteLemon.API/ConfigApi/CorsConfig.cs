namespace WhiteLemon.API.ConfigApi
{
    public static class CorsConfig
    {
        public static void AddCorsConfig(this IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }
    }
}
