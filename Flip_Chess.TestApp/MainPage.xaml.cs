using Microsoft.Maui.Controls;

namespace Flip_Chess.TestApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            base.BindingContext = this;
        }
    }
}