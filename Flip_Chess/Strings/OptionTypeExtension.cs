using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace Flip_Chess.Strings
{
    [ContentProperty(nameof(Type))]
    public class OptionTypeExtension : IMarkupExtension<OptionType>
    {
        public OptionType Type { get; set; }
        public OptionType ProvideValue(IServiceProvider serviceProvider) => this.Type;
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => (this as IMarkupExtension<OptionType>).ProvideValue(serviceProvider);
    }
}