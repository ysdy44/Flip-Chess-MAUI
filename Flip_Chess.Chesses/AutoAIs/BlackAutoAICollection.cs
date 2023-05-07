using Flip_Chess.Chesses.Extensions;

namespace Flip_Chess.Chesses.AutoAIs
{
    public sealed class BlackAutoAICollection : AutoAI
    {
        public BlackAutoAICollection(ChessType[,,] array) : base(array) { }
        internal BlackAutoAICollection(ChessType[,,] array, int parentZIndex, History history) : base(array, parentZIndex, history) { }

        protected override int DefaultValue() => int.MaxValue;
        protected override bool EqualsValue(int thanDefault, int amout) => thanDefault > amout;
        protected override void CreateHistory(ChessType[,,] array, int zIndex)
        {
            int h = array.Height();
            int w = array.Width();
            int count = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    ChessType item = array[zIndex, y, x];
                    if (item.IsBlack() is false) continue;
                    count++;

                    if (x > 0)
                    {
                        ChessType left = array[zIndex, y, x - 1];
                        if (item.BlackRelateTo(left) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new RedAutoAICollection(array, zIndex, new History(y, x, y, x - 1)));
                        }
                    }

                    if (y > 0)
                    {
                        ChessType top = array[zIndex, y - 1, x];
                        if (item.BlackRelateTo(top) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new RedAutoAICollection(array, zIndex, new History(y, x, y - 1, x)));
                        }
                    }

                    if (x + 1 < w)
                    {
                        ChessType right = array[zIndex, y, x + 1];
                        if (item.BlackRelateTo(right) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new RedAutoAICollection(array, zIndex, new History(y, x, y, x + 1)));
                        }
                    }

                    if (y + 1 < h)
                    {
                        ChessType bottom = array[zIndex, y + 1, x];
                        if (item.BlackRelateTo(bottom) is HistoryRelation.WeakEnemy)
                        {
                            base.Add(new RedAutoAICollection(array, zIndex, new History(y, x, y + 1, x)));
                        }
                    }
                }
            }

            if (count == 0) return;
            if (base.Count == 0)
            {
                base.Add(new RedAutoAICollection(array, zIndex, History.Noway));
            }
        }
    }
}