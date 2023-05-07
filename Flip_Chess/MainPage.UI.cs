using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Numerics;

namespace Flip_Chess
{
    partial class MainPage
    {
        public int SelectedIndex;// { get => this.FlipView.SelectedIndex; set => this.FlipView.SelectedIndex = value; }

        private string text1;
        public string Text1
        {
            get => this.text1;
            set
            {
                this.text1 = value;
                this.RunButton.Text = $"{this.text1}/{this.text2}";
            }
        }
        private string text2;
        public string Text2
        {
            get => this.text2;
            set
            {
                this.text2 = value;
                this.RunButton.Text = $"{this.text1}/{this.text2}";
            }
        }

        public int RedValue
        {
            get => (int)(this.RedProgressBar.HeightRequest / 4);
            set => this.RedProgressBar.HeightRequest = System.Math.Max(0, value * 4);
        }
        public int BlackValue
        {
            get => (int)(this.BlackProgressBar.HeightRequest / 4);
            set => this.BlackProgressBar.HeightRequest = System.Math.Max(0, value * 4);
        }

        public bool IsRedComputer
        {
            get => this.RedListBox.SelectedIndex == 0;
            set => this.RedListBox.SelectedIndex = value ? 0 : 1;
        }
        public bool IsBlackComputer
        {
            get => this.BlackListBox.SelectedIndex == 0;
            set => this.BlackListBox.SelectedIndex = value ? 0 : 1;
        }

        public GameMode Mode
        {
            get => (GameMode)this.ListBox.SelectedIndex;
            set => this.ListBox.SelectedIndex = (int)value;
        }

        #region DependencyProperty

        public GameState State
        {
            get => (GameState)base.GetValue(StateProperty);
            set => base.SetValue(StateProperty, value);
        }
        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(GameState), typeof(MainPage), GameState.None);

        #endregion

        public void ClickFullScreen()
        {
            /*
            if (this.ApplicationView.IsFullScreenMode)
            {
                this.ClickUnFullScreen();
            }
            else
            {
                VisualStateManager.GoToState(this, nameof(FullScreen), false);
                this.ApplicationView.TryEnterFullScreenMode();
            }
             */
        }
        public void ClickUnFullScreen()
        {
            /*
            VisualStateManager.GoToState(this, nameof(UnFullScreen), false);
            this.ApplicationView.ExitFullScreenMode();
            */
        }

        public void HideLines()
        {
            this.CenterHighlight.IsVisible = false;

            this.LeftHighlight.Hide();
            this.LeftHighlight.Stop();

            this.TopHighlight.Hide();
            this.TopHighlight.Stop();

            this.RightHighlight.Hide();
            this.RightHighlight.Stop();

            this.BottomHighlight.Hide();
            this.BottomHighlight.Stop();
        }
        public void ShowLinesAt(int y, int x, NeighborType type)
        {
            AbsoluteLayout.SetLayoutBounds(this.CenterHighlight, new Rect(x * 100, y * 100, 100, 100));
            this.CenterHighlight.IsVisible = true;

            if (type.HasFlag(NeighborType.Left))
            {
                if (this.LeftHighlight.CanAnimate) this.LeftHighlight.Begin();
                this.LeftHighlight.ShowAt(x * 100 - 100, y * 100);
            }
            else
                this.LeftHighlight.Hide();

            if (type.HasFlag(NeighborType.Top))
            {
                if (this.TopHighlight.CanAnimate) this.TopHighlight.Begin();
                this.TopHighlight.ShowAt(x * 100, y * 100 - 100);
            }
            else
                this.TopHighlight.Hide();

            if (type.HasFlag(NeighborType.Right))
            {
                if (this.RightHighlight.CanAnimate) this.RightHighlight.Begin();
                this.RightHighlight.ShowAt(x * 100 + 100, y * 100);
            }
            else
                this.RightHighlight.Hide();

            if (type.HasFlag(NeighborType.Bottom))
            {
                if (this.BottomHighlight.CanAnimate) this.BottomHighlight.Begin();
                this.BottomHighlight.ShowAt(x * 100, y * 100 + 100);
            }
            else
                this.BottomHighlight.Hide();
        }

        public Vector2 GetCemeteryPosition(ChessType toType)
        {
            if (toType.IsRed())
            {
                int i = -1;
                foreach (ChessDeaded item in this.RedCemetery)
                {
                    i++;
                    if (item.Type != toType) continue;

                    float visualX = (float)(this.RedCemeteryCanvas.X - this.Canvas.X);
                    float visualY = (float)(this.RedCemeteryCanvas.Y - this.Canvas.Y);
                    return new Vector2
                    {
                        X = visualX - 100 / 2 + 60 / 2,
                        Y = visualY + i * 60 - 100 / 2 + 60 / 2
                    };
                }
            }

            if (toType.IsBlack())
            {
                int i = -1;
                foreach (ChessDeaded item in this.BlackCemetery)
                {
                    i++;
                    if (item.Type != toType) continue;

                    float visualX = (float)(this.BlackCemeteryCanvas.X - this.Canvas.X);
                    float visualY = (float)(this.BlackCemeteryCanvas.Y - this.Canvas.Y);
                    return new Vector2
                    {
                        X = visualX - 100 / 2 + 60 / 2,
                        Y = visualY + i * 60 - 100 / 2 + 60 / 2
                    };
                }
            }

            return default;
        }

        public void SetMargin(int w, int h, int margin)
        {
            if (w > h + margin)
            {
                double scaleX = 1d * (w - margin - margin) / (80 + 28 + this.W + 28 + 80);
                double scaleY = 1d * (h - margin) / (28 + this.H + 28);
                double scale = System.Math.Min(scaleX, scaleY);
                this.Viewbox.Scale = System.Math.Max(0.1, scale);
            }
            else if (w < h - margin)
            {
                double scaleX = 1d * (w) / (80 + 28 + this.W + 28 + 80);
                double scaleY = 1d * (h - margin - margin) / (28 + this.H + 28);
                double scale = System.Math.Min(scaleX, scaleY);
                this.Viewbox.Scale = System.Math.Max(0.1, scale);
            }
            else
            {
                double scaleX = 1d * (w - margin - margin) / (80 + 28 + this.W + 28 + 80);
                double scaleY = 1d * (h - margin - margin) / (28 + this.H + 28);
                double scale = System.Math.Min(scaleX, scaleY);
                this.Viewbox.Scale = System.Math.Max(0.1, scale);
            }
        }

        private bool CanFlip => this.FlipItem.CanAnimate;
        private void StopFlip() => this.FlipItem.Stop();
        public void BeginFlip() => this.FlipItem.Begin();
        private void HideFlip() => this.FlipItem.Hide();
        public void ShowFlip(Vector2 position, string uri) => this.FlipItem.Show(position, uri);

        private bool CanCapture => this.CaptureItem.CanAnimate;
        private void StopCapture() => this.CaptureItem.Stop();
        public void BeginCapture() => this.CaptureItem.Begin();
        private void HideCapture() => this.CaptureItem.Hide();
        public void ShowCapture(Vector2 from, Vector2 to, string uri) => this.CaptureItem.Show(from, to, uri);

        private bool CanCemetery => this.CemeteryItem.CanAnimate;
        private void StopCemetery() => this.CemeteryItem.Stop();
        public void BeginCemetery() => this.CemeteryItem.Begin();
        private void HideCemetery() => this.CemeteryItem.Hide();
        public void ShowCemetery(Vector2 from, Vector2 to, string uri) => this.CemeteryItem.Show(from, to, uri);

        private bool CanClip => this.ClipContainer.CanAnimate;
        public void StopClip() => this.ClipContainer.Stop();
        public void BeginClip() => this.ClipContainer.Begin();
    }
}