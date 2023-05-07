using Microsoft.Maui.Controls;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public sealed class CommandListView : ListView
    {

        #region DependencyProperty

        public ICommand Command
        {
            get => (ICommand)base.GetValue(CommandProperty);
            set => base.SetValue(CommandProperty, value);
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CommandListView));

        #endregion

        public CommandListView()
        {
            base.ItemSelected += (s, e) =>
            {
                if (base.IsLoaded is false) return;
                this.Command?.Execute(e.SelectedItem); // Command
            };
        }
    }
}