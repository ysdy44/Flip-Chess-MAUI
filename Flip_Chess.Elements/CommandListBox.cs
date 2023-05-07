using Microsoft.Maui.Controls;
using System;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public sealed class CommandListBox : ListView
    {

        #region DependencyProperty

        public object CommandParameter
        {
            get => (object)base.GetValue(CommandParameterProperty);
            set => base.SetValue(CommandParameterProperty, value);
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(CommandListBox));

        public ICommand Command
        {
            get => (ICommand)base.GetValue(CommandProperty);
            set => base.SetValue(CommandProperty, value);
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(CommandListBox));

        #endregion

        public int SelectedIndex
        {
            get
            {
                int index = 0;
                foreach (object item in base.ItemsSource)
                {
                    if (item == base.SelectedItem)
                    {
                        return index;
                    }
                    index++;
                }
                return -1;
            }
            set
            {
                int index = 0;
                foreach (object item in base.ItemsSource)
                {
                    if (index == value)
                    {
                        base.SelectedItem = item;
                        return;
                    }
                    index++;
                }
            }
        }

        public CommandListBox()
        {
            base.ItemSelected += (s, e) =>
            {
                if (base.IsLoaded is false) return;
                this.Command?.Execute(this.CommandParameter); // Command
            };
        }
    }
}