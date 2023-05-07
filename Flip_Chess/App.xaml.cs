using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace Flip_Chess
{
    public partial class App : Application
    {
        //@Const
        internal const int Width = 4;
        internal const int Height = 8;

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
            // 9 Icons:
            // <FontImageSource x:Name="Continue" Glyph="&#xE102;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Pause" Glyph="&#xE103;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Next" Glyph="&#xE111;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Computer" Glyph="&#xE115;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Mode" Glyph="&#xE138;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Players" Glyph="&#xE13d;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="Restart" Glyph="&#xE149;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="UnFullScreen" Glyph="&#xE1d8;" FontFamily="Segoe UI Symbol"/>
            // <FontImageSource x:Name="FullScreen" Glyph="&#xE1d9;" FontFamily="Segoe UI Symbol"/>
            fonts.AddFont("Segoe UI Symbol Regular.ttf", "Segoe UI Symbol");

            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        }
    }
}