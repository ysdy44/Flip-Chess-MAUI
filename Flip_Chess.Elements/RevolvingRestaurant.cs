using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Flip_Chess.Elements
{
    public sealed class RevolvingRestaurant : Grid
    {
        //readonly Rotater R0 = new Rotater(0);
        //readonly Rotater R1 = new Rotater(90);
        //readonly Rotater R2 = new Rotater(180);
        //readonly Rotater R3 = new Rotater(270);

        //readonly string[] Text = new string[]
        //{
        //    "中", "国", "象", "棋", "翻", "翻", "棋"
        //};

        readonly Animation Storyboard;

        public RevolvingRestaurant()
        {
            this.Storyboard = new Animation(this.Animate, 0, 1);
            base.Loaded += (s, e) =>
            {
                if (base.IsEnabled)
                {
                    this.Begin(); // Storyboard
                }
            };

            base.PropertyChanged += (s, e) =>
            {
                if (base.IsLoaded is false) return;

                if (e.PropertyName == nameof(IsEnabled))
                {
                    if (base.IsEnabled)
                    {
                        this.Begin(); // Storyboard
                    }
                    else
                    {
                        this.Stop(); // Storyboard
                    }
                }
            };
        }

        private bool Repeat() => true;
        private void Animate(double progress) => base.Rotation = 360 - 360 * progress;
        private void Completed(double progress, bool finished)
        {
        }

        private bool Stop() => this.AbortAnimation(nameof(Storyboard));
        private void Begin() => this.Storyboard.Commit(this, nameof(Storyboard), 16, 24000, Easing.Linear, this.Completed, this.Repeat);
    }
}