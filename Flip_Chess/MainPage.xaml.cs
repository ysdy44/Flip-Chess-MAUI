using Microsoft.Maui.Controls;

namespace Flip_Chess
{
    public sealed partial class MainPage : ContentPage
    { 
        public MainPage()
        {
            this.InitializeComponent();
            base.BindingContext = this;
        }
    }
}