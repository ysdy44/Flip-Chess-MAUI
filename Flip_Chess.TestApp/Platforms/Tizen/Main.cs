using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Flip_Chess.TestApp
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => App.CreateMauiApp();

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}