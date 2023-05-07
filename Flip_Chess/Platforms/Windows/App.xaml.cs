using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Flip_Chess.WinUI
{
    public partial class App : MauiWinUIApplication
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => Flip_Chess.App.CreateMauiApp();
    }
}