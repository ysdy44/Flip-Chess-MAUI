using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Numerics;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public partial class AnimationZoomImage : Image
    {

        #region DependencyProperty

        public object CommandParameter
        {
            get => (object)base.GetValue(CommandParameterProperty);
            set => base.SetValue(CommandParameterProperty, value);
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AnimationZoomImage));

        public ICommand Command
        {
            get => (ICommand)base.GetValue(CommandProperty);
            set => base.SetValue(CommandProperty, value);
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimationZoomImage));

        #endregion

        readonly FileImageSource FileImageSource = new FileImageSource();
        readonly Animation HideStoryboard;
        readonly Animation ShowStoryboard;

        public bool CanAnimate => true; //  this.HideStoryboard.IsEnabled && this.ShowStoryboard.IsEnabled;

        public string PlaceholderSource { get; set; }
        public string ImageSource { get; private set; }

        public AnimationZoomImage()
        {
            base.Source = this.FileImageSource;
            this.HideStoryboard = new Animation(this.HideAnimate, 0, 1);
            this.ShowStoryboard = new Animation(this.ShowAnimate, 0, 1);
        }

        #region IHideAnimation

        private bool HideRepeat() => false;
        private void HideAnimate(double progress) => this.Scale = 1 - 0.6 * progress;
        private void HideCompleted(double progress, bool finished)
        {
            this.FileImageSource.File = this.ImageSource;

            this.ShowBegin(); // Storyboard
        }

        private bool HideStop() => this.AbortAnimation(nameof(HideStoryboard));
        private void HideBegin() => this.HideStoryboard.Commit(this, nameof(HideStoryboard), 16, 100, Easing.CubicIn, this.HideCompleted, this.HideRepeat);

        #endregion

        #region IShowAnimation

        private bool ShowRepeat() => false;
        private void ShowAnimate(double progress) => this.Scale = 0.4 + 0.6 * progress;
        private void ShowCompleted(double progress, bool finished)
        {
            this.Command?.Execute(this.CommandParameter); // Command

            this.Hide();
        }

        private bool ShowStop() => this.AbortAnimation(nameof(ShowStoryboard));
        private void ShowBegin() => this.ShowStoryboard.Commit(this, nameof(ShowStoryboard), 16, 100, Easing.CubicIn, this.ShowCompleted, this.ShowRepeat);

        #endregion

        public void Stop()
        {
            this.HideStop(); // Storyboard
            this.ShowStop(); // Storyboard
        }
        public void Begin()
        {
            this.HideBegin(); // Storyboard
        }

        public void Hide()
        {
            base.IsVisible = false;
            this.FileImageSource.File = this.PlaceholderSource;
        }
        public void Show(Vector2 position, string uri)
        {
            base.Scale = 1;

            base.TranslationX = position.X;
            base.TranslationY = position.Y;

            this.ImageSource = uri;
            this.FileImageSource.File = this.PlaceholderSource;
            base.IsVisible = true;
        }
    }
}