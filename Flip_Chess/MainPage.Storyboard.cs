using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Models;
using Microsoft.Maui;

namespace Flip_Chess
{
    partial class MainPage
    {
        private void Shown()
        {
            this.Chesses.Copy(this.Collection);

            foreach (ChessAlive item in this.Chesses)
            {
                item.Visibility = true;
            }
        }

        private void Relive()
        {
            foreach (ChessDeaded item in this.RedCemetery)
            {
                item.Count = 0;
            }

            foreach (ChessDeaded item in this.BlackCemetery)
            {
                item.Count = 0;
            }

            this.RedValue = 52;
            this.BlackValue = 52;
        }

        private void Deaded()
        {
            int h = this.Collection.Height();
            int w = this.Collection.Width();

            int red = 0;
            int black = 0;

            foreach (ChessDeaded item in this.RedCemetery)
            {
                int count = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        ChessType type = this.Collection[0, y, x];
                        if (type == default) continue;
                        if (type == item.Type) count--;

                        int i = w.IndexOf(y, x);
                        if (this.Randoms[i].Type == item.Type) count++;
                    }
                }
                item.Count = count;
                if (count > 0) red += count * item.Type.GetLevelAbs();
            }

            foreach (ChessDeaded item in this.BlackCemetery)
            {
                int count = 0;
                for (int y = 0; y < h; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        ChessType type = this.Collection[0, y, x];
                        if (type == default) continue;
                        if (type == item.Type) count--;

                        int i = w.IndexOf(y, x);
                        if (this.Randoms[i].Type == item.Type) count++;
                    }
                }
                item.Count = count;
                if (count > 0) black += count * item.Type.GetLevelAbs();
            }

            this.RedValue = 52 - red;
            this.BlackValue = 52 - black;
        }
    }
}