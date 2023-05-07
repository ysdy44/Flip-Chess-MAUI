using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Microsoft.Maui.Controls;

namespace Flip_Chess.TestApp
{
    partial class MainPage
    {
        public void ItemClick(History history)
        {
            if (history.Distance is HistoryDistance.Others) return;

            this.Timer.Stop();
            this.Timer.Start();

            this.Historian.Add(history);

            switch ((HistoryAction)history.Distance)
            {
                case HistoryAction.Noway:
                    {
                        this.Randoms.Home();
                        this.Randoms.Random();

                        this.Collection.Clear();
                        this.Chesses.Copy(this.Collection);

                        this.Historian.Clear();
                    }
                    break;
                case HistoryAction.Flip:
                    {
                        // Info
                        int index = this.Collection.IndexOf(history.Y, history.X);
                        ChessType random = this.Randoms[index].Type;
                        this.Collection[0, history.Y, history.X] = random;

                        // UI
                        this.Chesses.Copy(this.Collection);
                    }
                    break;
                case HistoryAction.Capture:
                    {
                        // Info
                        ChessType fromType = this.Collection[0, history.Y1, history.X1];

                        this.Collection[0, history.Y1, history.X1] = ChessType.Deaded;
                        this.Collection[0, history.Y2, history.X2] = fromType;

                        // UI
                        this.Chesses.Copy(this.Collection);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}