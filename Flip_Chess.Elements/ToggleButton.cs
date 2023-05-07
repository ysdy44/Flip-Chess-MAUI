using Microsoft.Maui.Controls;

namespace Flip_Chess.Elements
{
    public sealed class ToggleButton : Button
    {
        #region DependencyProperty

        public bool IsChecked
        {
            get => (bool)base.GetValue(IsCheckedProperty);
            set => base.SetValue(IsCheckedProperty, value);
        }
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CommandListView), false);

        #endregion

        public ToggleButton( )
        {
            base.Clicked += (s, e) =>
            {
                this.IsChecked = !this.IsChecked;
            };
        }
    }
}