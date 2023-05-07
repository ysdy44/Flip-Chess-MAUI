using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Windows.Input;

namespace Flip_Chess.Elements
{
    public abstract class LinesCanvas : AbsoluteLayout
    {
        protected LinesCanvas(int w, int h, int unit)
        {
            base.WidthRequest = w * unit;
            base.HeightRequest = h * unit;

            // X-Coordinate
            for (int i = 0; i <= w; i++)
            {
                base.Children.Add(new Line { X1 = i * unit, Y1 = 0, X2 = i * unit, Y2 = h * unit });
            }
            // Y-Coordinate
            for (int i = 0; i <= h; i++)
            {
                base.Children.Add(new Line { X1 = 0, Y1 = i * unit, X2 = w * unit, Y2 = i * unit });
            }

            //LeftTop
            Corner lt = new Corner(0, 0);
            base.Children.Add(new Line { X1 = lt.R, Y1 = lt.B, X2 = lt.R, Y2 = lt.BB });
            base.Children.Add(new Line { X1 = lt.R, Y1 = lt.B, X2 = lt.RR, Y2 = lt.B });

            // LeftBottom
            Corner lb = new Corner(0, h * unit);
            base.Children.Add(new Line { X1 = lb.R, Y1 = lb.T, X2 = lb.R, Y2 = lb.TT });
            base.Children.Add(new Line { X1 = lb.R, Y1 = lb.T, X2 = lb.RR, Y2 = lb.T });

            // RightTop
            Corner rt = new Corner(w * unit, 0);
            base.Children.Add(new Line { X1 = rt.L, Y1 = rt.B, X2 = rt.L, Y2 = rt.BB });
            base.Children.Add(new Line { X1 = rt.L, Y1 = rt.B, X2 = rt.LL, Y2 = rt.B });

            // RightBottom
            Corner rb = new Corner(w * unit, h * unit);
            base.Children.Add(new Line { X1 = rb.L, Y1 = rb.T, X2 = rb.L, Y2 = rb.TT });
            base.Children.Add(new Line { X1 = rb.L, Y1 = rb.T, X2 = rb.LL, Y2 = rb.T });

            for (int i = 1; i < h / 2; i++)
            {
                // Left Lines
                Corner l = new Corner(0, i * 2 * unit);
                base.Children.Add(new Line { X1 = l.R, Y1 = l.T, X2 = l.R, Y2 = l.TT });
                base.Children.Add(new Line { X1 = l.R, Y1 = l.T, X2 = l.RR, Y2 = l.T });
                base.Children.Add(new Line { X1 = l.R, Y1 = l.B, X2 = l.R, Y2 = l.BB });
                base.Children.Add(new Line { X1 = l.R, Y1 = l.B, X2 = l.RR, Y2 = l.B });

                // Right Lines
                Corner r = new Corner(w * unit, i * 2 * unit);
                base.Children.Add(new Line { X1 = r.L, Y1 = r.T, X2 = r.L, Y2 = r.TT });
                base.Children.Add(new Line { X1 = r.L, Y1 = r.T, X2 = r.LL, Y2 = r.T });
                base.Children.Add(new Line { X1 = r.L, Y1 = r.B, X2 = r.L, Y2 = r.BB });
                base.Children.Add(new Line { X1 = r.L, Y1 = r.B, X2 = r.LL, Y2 = r.B });
            }

            // Center Lines
            int xp = 2; //Padding
            int xs = 2; // Scale
            int yp = 1; //Padding
            int ys = 3; // Scale
            for (int y = 0; y < (h - yp - yp + ys) / ys; y++)
            {
                var y3 = y * ys + yp;
                for (int x = 0; x < (w - xp - xp + xs) / xs; x++)
                {
                    var x2 = x * xs + xp;

                    Corner c = new Corner(x2 * unit, y3 * unit);
                    base.Children.Add(new Line { X1 = c.L, Y1 = c.T, X2 = c.L, Y2 = c.TT });
                    base.Children.Add(new Line { X1 = c.L, Y1 = c.T, X2 = c.LL, Y2 = c.T });
                    base.Children.Add(new Line { X1 = c.R, Y1 = c.T, X2 = c.R, Y2 = c.TT });
                    base.Children.Add(new Line { X1 = c.R, Y1 = c.T, X2 = c.RR, Y2 = c.T });
                    base.Children.Add(new Line { X1 = c.R, Y1 = c.B, X2 = c.R, Y2 = c.BB });
                    base.Children.Add(new Line { X1 = c.R, Y1 = c.B, X2 = c.RR, Y2 = c.B });
                    base.Children.Add(new Line { X1 = c.L, Y1 = c.B, X2 = c.L, Y2 = c.BB });
                    base.Children.Add(new Line { X1 = c.L, Y1 = c.B, X2 = c.LL, Y2 = c.B });
                }
            }
        }
    }
}