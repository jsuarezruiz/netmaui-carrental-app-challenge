using CarRentalApp.Controls;
using CarRentalApp.Services;
using CarRentalApp.ViewModels;
using CarRentalApp.Views;
using Microsoft.Extensions.Logging;

namespace CarRentalApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiEvergine()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<CarService>();
            builder.Services.AddSingleton<HomeView>();
            builder.Services.AddSingleton<HomeViewModel>();

            return builder.Build();
        }
    }
}
