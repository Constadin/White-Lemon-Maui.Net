using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using WhiteLemonMauiUI.Api.ApiConfigMaui;
using WhiteLemonMauiUI.Pages.ViewModels;

namespace WhiteLemonMauiUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PlayfairDisplay-Bold.ttf", "PlayfairDisplayBold");
                    fonts.AddFont("PlayfairDisplay-BoldItalic.ttf", "PlayfairDisplayBoldItalic");
                    fonts.AddFont("PlayfairDisplay-Regular.ttf", "PlayfairDisplayRegular");
                    fonts.AddFont("PlayfairDisplay-Medium.ttf", "PlayfairDisplayMedium");
                });

            
            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();


            // Adding HttpClient with custom handler for SSL issues
            builder.Services.AddSingleton(sp =>
            {
                var httpClientHandler = new HttpClientHandler
                {
                    // Ignore SSL validation, only useful in a dev environment.
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };

                return new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(ApiConfig.BaseAddress), // Setting the API base URL
                    Timeout = TimeSpan.FromSeconds(30) // Setting timeout for requests
                };
            });

            // Registering other UI-related services
            builder.Services.RegisterUiServices();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void ConfigureRefit(IServiceCollection services)
        {
            var baseApiUrl = "https://05rgtd5d-7091.euw.devtunnels.ms";
        }

    }
}
