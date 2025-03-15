using Microsoft.Extensions.FileProviders;
using WhiteLemon.API.ConfigApi;
using WhiteLemon.API.Endpoints;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var builder = WebApplication.CreateBuilder(args);

// Add builder services to the container.

builder.AddBuilderDependencies();

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7091); // HTTPs
//    options.ListenAnyIP(5049); // HTTP
//});

// Build the application

var app = builder.Build();

app.UseOpenApi();

app.UseCors("AllowAll");


/// <summary>
/// Redirects HTTP requests to HTTPS.
/// Ανακατευθύνει τα HTTP αιτήματα σε HTTPS.
/// </summary>
//app.UseHttpsRedirection();

/// <summary>
/// Maps controllers to handle the incoming HTTP requests.
/// Χαρτογραφεί τους controllers για να χειριστούν τα εισερχόμενα HTTP αιτήματα.
/// </summary>
app.MapControllers();

app.AddRootEndpoints();


/// <summary>
/// Starts the application, making it listen for HTTP requests.
/// Ξεκινά την εφαρμογή, κάνοντάς την να ακούει για HTTP αιτήματα.
/// </summary>
app.Run();



