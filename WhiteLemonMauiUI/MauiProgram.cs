using CommunityToolkit.Maui;
using FFImageLoading.Maui;
using Microsoft.Extensions.Logging;
using WhiteLemon.Shared.Constants;
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
                .UseFFImageLoading()
                .ConfigureFonts(fonts =>
                {
                    //fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    //fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PlayfairDisplay-Bold.ttf", "PlayfairDisplayBold");
                    fonts.AddFont("PlayfairDisplay-BoldItalic.ttf", "PlayfairDisplayBoldItalic");
                    fonts.AddFont("PlayfairDisplay-Regular.ttf", "PlayfairDisplayRegular");
                    fonts.AddFont("PlayfairDisplay-Medium.ttf", "PlayfairDisplayMedium");
                });

            // Registering ViewModels
            // Εγγραφή ViewModels

            builder.Services.AddSingleton<RegisterViewModel>();
            builder.Services.AddSingleton<LoginViewModel>();
            builder.Services.AddSingleton<HomePageViewModel>();
            builder.Services.AddSingleton<DiscoverPeopleViewModel>();
            builder.Services.AddSingleton<ProfileViewModel>();

            // Add HttpClient with Custom Handler
            // Προσθήκη HttpClient με Custom Handler

            builder.Services.AddHttpClient(Const.NameOfCliendApi, client =>
            {
                client.BaseAddress = new Uri(ApiConfig.BaseAddress); // API Base URL
                client.Timeout = TimeSpan.FromSeconds(30); // Timeout για τις κλήσεις
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                return new HttpClientHandler

                {   // Ignore SSL validation (for DEV environment)
                    // Αγνόηση SSL validation (για DEV περιβάλλον)

                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
            });

            // Registering other UI-related services
            // Καταχώρηση άλλων υπηρεσιών 

            builder.Services.RegisterUiServices();
            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }


        //devtunnels.ms
        private static void ConfigureRefit(IServiceCollection services)
        {
            var baseApiUrl = "https://05rgtd5d-7091.euw.devtunnels.ms";
        }

    }
}
