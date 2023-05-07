using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Flip_Chess
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        public static MauiApp CreateMauiApp()
        {
            MauiAppBuilder builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>();
            builder.ConfigureFonts(App.ConfigureFonts);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static void ConfigureFonts(IFontCollection fonts)
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }
    }
}