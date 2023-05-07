using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace Flip_Chess.Elements
{
    public partial class Highlight : AbsoluteLayout
    {
        bool IsRepeat;

        readonly Animation Storyboard;

        public bool CanAnimate => true; //  this.Storyboard.IsEnabled;

        public Highlight()
        {
            this.InitializeComponent();
            this.Storyboard = new Animation(this.Animate, 0, 1);
        }

        private bool Repeat() => true;
        private void Animate(double progress) => this.Opacity = this.IsRepeat ? 1 - progress : progress;
        private void Completed(double progress, bool finished)
        {
            this.Opacity = 1;
            this.IsRepeat = !this.IsRepeat;
        }

        public bool Stop() => this.AbortAnimation(nameof(Storyboard));
        public void Begin() => this.Storyboard.Commit(this, nameof(Storyboard), 16, 600, Easing.Linear, this.Completed, this.Repeat);

        public void Hide()
        {
            base.IsVisible = false;
            this.Stop(); // Storyboard
        }
        public void ShowAt(int x, int y)
        {
            AbsoluteLayout.SetLayoutBounds(this, new Rect(x, y, 100, 100));
            base.IsVisible = true;
            this.Begin(); // Storyboard
        }
    }
}