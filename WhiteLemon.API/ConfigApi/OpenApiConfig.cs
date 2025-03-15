using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Scalar.AspNetCore;
using WhiteLemon.API.Interfaces;
using WhiteLemon.API.Services;
using WhiteLemon.Application;
using WhiteLemon.Infrastructure;
using WhiteLemon.Infrastructure.Data;

namespace WhiteLemon.API.ConfigApi
{
    public static class OpenApiConfig
    {
        public static void AddOpenApiServices(this IServiceCollection services)
        {
            services.AddOpenApi();

            /// <summary>
            /// Registers application services such as IUserService and others.
            /// Εγγράφει τις υπηρεσίες της εφαρμογής όπως το IUserService και άλλες.
            /// </summary>
            services.AddApplicationServices();
            services.AddInfrastructureServices();

            /// <summary>
            /// Enables Swagger, which helps generate API documentation and testing UI.
            /// Ενεργοποιεί το Swagger, το οποίο βοηθά στη δημιουργία τεκμηρίωσης API και UI για τη δοκιμή.
            /// </summary>
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddScoped<IAddPhotoUserService, AddPhotoUserService>();
        }

        public static void UseOpenApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                //app.MapScalarApiReference(options =>
                //{
                //    options.Title = "White Lemon API";
                //    options.Theme = ScalarTheme.Kepler;
                //    options.HideClientButton = true;
                //});

                app.UseSwagger();
                app.UseSwaggerUI();


            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
                RequestPath = "/Uploads"
            });

        }


    }
}
