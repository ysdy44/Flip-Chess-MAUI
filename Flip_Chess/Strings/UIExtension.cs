using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace Flip_Chess.Strings
{
    [ContentProperty(nameof(Type))]
    public class UIExtension : IMarkupExtension<string>
    {
        public UIType Type { get; set; }
        public string ProvideValue(IServiceProvider serviceProvider) => Strings.UI.ResourceManager.GetString(this.Type.ToString());
        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider) => (this as IMarkupExtension<string>).ProvideValue(serviceProvider);
    }
}