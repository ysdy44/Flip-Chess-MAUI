using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Flip_Chess.Elements
{
    public partial class AnimationFadeContainer : Border
    {
        readonly Animation HideStoryboard;
        readonly Animation ShowStoryboard;

        public bool CanAnimate => true; //  this.HideStoryboard.IsEnabled && this.ShowStoryboard.IsEnabled;

        public AnimationFadeContainer()
        {
            this.HideStoryboard = new Animation(this.HideAnimate, 0, 1);
            this.ShowStoryboard = new Animation(this.ShowAnimate, 0, 1);

            base.IsVisible = false;
            base.Opacity = 0;
            base.Loaded += (s, e) =>
            {
                if (base.IsEnabled)
                {
                    base.IsVisible = true;
                    base.Opacity = 1;
                }
            };

            base.PropertyChanged += (s, e) =>
            {
                if (base.IsLoaded is false) return;

                if (e.PropertyName == nameof(IsEnabled))
                {
                    if (base.IsEnabled)
                    {
                        base.IsVisible = true;
                        base.Opacity = 0;
                        this.ShowBegin(); // Storyboard
                    }
                    else
                    {
                        base.IsVisible = true;
                        base.Opacity = 1;
                        this.HideBegin(); // Storyboard
                    }
                }
            };
        }

        #region IHideAnimation

        private bool HideRepeat() => false;
        private void HideAnimate(double progress) => base.Opacity = (1 - progress);
        private void HideCompleted(double progress, bool finished)
        {
            base.IsVisible = false;
        }

        private bool HideStop() => this.AbortAnimation(nameof(HideStoryboard));
        private void HideBegin() => this.HideStoryboard.Commit(this, nameof(HideStoryboard), 16, 200, Easing.Linear, this.HideCompleted, this.HideRepeat);

        #endregion

        #region IShowAnimation

        private bool ShowRepeat() => false;
        private void ShowAnimate(double progress) => base.Opacity = progress;
        private void ShowCompleted(double progress, bool finished)
        {
        }

        private bool ShowStop() => this.AbortAnimation(nameof(ShowStoryboard));
        private void ShowBegin() => this.ShowStoryboard.Commit(this, nameof(ShowStoryboard), 16, 200, Easing.Linear, this.ShowCompleted, this.ShowRepeat);

        #endregion
    }
}