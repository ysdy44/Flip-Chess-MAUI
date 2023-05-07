using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public partial class AnimationClipContainer : AbsoluteLayout
    {

        #region DependencyProperty

        public object CommandParameter
        {
            get => (object)base.GetValue(CommandParameterProperty);
            set => base.SetValue(CommandParameterProperty, value);
        }
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(AnimationClipContainer));

        public ICommand Command
        {
            get => (ICommand)base.GetValue(CommandProperty);
            set => base.SetValue(CommandProperty, value);
        }
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AnimationClipContainer));

        #endregion

        readonly RectangleGeometry Geometry;
        readonly Animation HideStoryboard;
        readonly Animation ShowStoryboard;

        public bool CanAnimate => true; //  true; //  this.HideStoryboard.IsEnabled && this.ShowStoryboard.IsEnabled;

        public AnimationClipContainer()
        {
            this.Geometry = new RectangleGeometry();
            base.Clip = this.Geometry;
            this.HideStoryboard = new Animation(this.HideAnimate, 0, 1);
            this.ShowStoryboard = new Animation(this.ShowAnimate, 0, 1);
            base.SizeChanged += (s, e) =>
            {
                this.Geometry.Rect = new Rect(0, 0, base.Width, base.Height);
            };
        }

        #region IHideAnimation

        private bool HideRepeat() => false;
        private void HideAnimate(double progress) => this.Geometry.Rect = new Rect(0, 0, base.Width, base.Height * (1 - progress));
        private void HideCompleted(double progress, bool finished)
        {
            this.Geometry.Rect = new Rect(0, 0, base.Width, 0);
            this.Command?.Execute(this.CommandParameter); // Command
            this.ShowBegin(); // Storyboard
        }

        private bool HideStop() => this.AbortAnimation(nameof(HideStoryboard));
        private void HideBegin() => this.HideStoryboard.Commit(this, nameof(HideStoryboard), 16, 200, Easing.Linear, this.HideCompleted, this.HideRepeat);

        #endregion

        #region IShowAnimation

        private bool ShowRepeat() => false;
        private void ShowAnimate(double progress) => this.Geometry.Rect = new Rect(0, 0, base.Width, base.Height * progress);
        private void ShowCompleted(double progress, bool finished)
        {
            this.Geometry.Rect = new Rect(0, 0, base.Width, base.Height);
        }

        private bool ShowStop() => this.AbortAnimation(nameof(ShowStoryboard));
        private void ShowBegin() => this.ShowStoryboard.Commit(this, nameof(ShowStoryboard), 16, 200, Easing.Linear, this.ShowCompleted, this.ShowRepeat);

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
    }
}