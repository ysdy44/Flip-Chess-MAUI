using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Numerics;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public partial class AnimationZoomMoveImage : Image
    {

        #region DependencyProperty

        public object CommandParameter
        {
            get => (object)base.GetValue(CommandParameterProperty);
            set => base.SetValue(CommandParameterProperty, value);
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AnimationZoomMoveImage));

        public ICommand Command
        {
            get => (ICommand)base.GetValue(CommandProperty);
            set => base.SetValue(CommandProperty, value);
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimationZoomMoveImage));

        #endregion

        Vector2 From;
        Vector2 To;

        readonly FileImageSource FileImageSource = new FileImageSource();
        readonly Animation Storyboard;

        public bool CanAnimate => true; //  this.Storyboard.IsEnabled;

        public AnimationZoomMoveImage()
        {
            base.Source = this.FileImageSource;
            this.Storyboard = new Animation(this.Animate, 0, 1);
        }

        private bool Repeat() => false;
        private void Animate(double progress)
        {
            base.TranslationX = this.From.X * (1 - progress) + this.To.X * progress;
            base.TranslationY = this.From.Y * (1 - progress) + this.To.Y * progress;
            base.Scale = 1 - 0.4 * progress;
        }
        private void Completed(double progress, bool finished)
        {
            this.Command?.Execute(this.CommandParameter); // Command
        }

        public bool Stop() => this.AbortAnimation(nameof(Storyboard));
        public void Begin() => this.Storyboard.Commit(this, nameof(Storyboard), 16, 600, Easing.CubicOut, this.Completed, this.Repeat);

        public void Hide()
        {
            base.IsVisible = false;
            this.FileImageSource.File = null;
        }
        public void Show(Vector2 from, Vector2 to, string uri)
        {
            base.Scale = 0.8;

            base.TranslationX = from.X;
            base.TranslationY = from.Y;

            this.From = from;

            this.To = to;

            this.FileImageSource.File = uri;
            base.IsVisible = true;
        }
    }
}