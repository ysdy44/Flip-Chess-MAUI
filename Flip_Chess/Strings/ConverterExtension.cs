using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Globalization;

namespace Flip_Chess.Strings
{
    [ContentProperty(nameof(IValueConverter))]
    public class ConverterExtension : IMarkupExtension<IValueConverter>, IValueConverter
    {
        public ConverterType Type { get; set; }
        public IValueConverter ProvideValue(IServiceProvider serviceProvider) => this;
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => (this as IMarkupExtension<IValueConverter>).ProvideValue(serviceProvider);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (this.Type)
            {
                case ConverterType.Name:
                    return value;
                    
                case ConverterType.NoneToBooleanConverter:
                    if (value is GameState item1)
                    {
                        return item1 == default;
                    }
                    else
                    {
                        return true;
                    }
                case ConverterType.ReverseNoneToBooleanConverter:
                    if (value is GameState item2)
                    {
                        return item2 != default;
                    }
                    else
                    {
                        return false;
                    }

                case ConverterType.LoseToVisibilityConverter:
                    if (value is GameState item3)
                    {
                        return item3 == GameState.Lose;
                    }
                    else
                    {
                        return false;
                    }
                case ConverterType.WinToVisibilityConverter:
                    if (value is GameState item4)
                    {
                        return item4 == GameState.Win;
                    }
                    else
                    {
                        return false;
                    }
                case ConverterType.PauseToVisibilityConverter:
                    if (value is GameState item5)
                    {
                        return item5 == GameState.Pause;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return value;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (this.Type)
            {
                case ConverterType.Name:
                    return value;

                case ConverterType.NoneToBooleanConverter:
                    if (value is bool item1)
                    {
                        return item1 ? GameState.None : GameState.Pause;
                    }
                    else
                    {
                        return GameState.None;
                    }
                case ConverterType.ReverseNoneToBooleanConverter:
                    if (value is bool item2)
                    {
                        return item2 ? GameState.Pause : GameState.None;
                    }
                    else
                    {
                        return GameState.Pause;
                    }

                case ConverterType.LoseToVisibilityConverter:
                    if (value is bool item3)
                    {
                        return item3 ? GameState.Lose : GameState.None;
                    }
                    else
                    {
                        return GameState.None;
                    }
                case ConverterType.WinToVisibilityConverter:
                    if (value is bool item4)
                    {
                        return item4 ? GameState.Win : GameState.None;
                    }
                    else
                    {
                        return GameState.None;
                    }
                case ConverterType.PauseToVisibilityConverter:
                    if (value is bool item5)
                    {
                        return item5 ? GameState.Pause : GameState.None;
                    }
                    else
                    {
                        return GameState.None;
                    }

                default:
                    return value;
            }
        }
    }
}