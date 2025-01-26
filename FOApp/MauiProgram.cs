//using Microsoft.Maui.Controls.Maps;
//using Microsoft.Maui.Maps;
using FOApp.Data;
using FOApp.Views;
//using Map = Microsoft.Maui.Controls.Maps.Map;
using FOApp.Services;
using Maui.GoogleMaps.Hosting;
using Maui.GoogleMaps.Clustering.Hosting;

namespace FOApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            //.UseMauiMaps();// Nếu dùng androi thì bật
#if ANDROID
        builder.UseGoogleMaps();        
#elif IOS
        builder.UseGoogleMaps("AIzaSyCvARxyqOR8SQB-X8jMHNnRSFLZdSeEmM8");
#endif

            //builder.Services.AddSingleton<FiberDataPage>();
            builder.Services.AddSingleton<FiberListPage>();            
            //builder.Services.AddTransient<FiberItemPage>();

            builder.Services.AddSingleton<FiberItemDatabase>();
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddLogging();
            builder.UseGoogleMapsClustering();

            return builder.Build();
        }
    }
}