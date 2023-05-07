using Flip_Chess.Chesses.Extensions;
using System;
using System.Collections.Generic;

namespace Flip_Chess.Chesses.AutoAIs
{
    public abstract partial class AutoAI : List<AutoAI>, IDisposable
    {
        static int ZIndexInstance;

        readonly int ZIndex;
        readonly History History;
        readonly int LevelSquared;

        internal AutoAI(ChessType[,,] array, int parentZIndex, History history)
        {
            // 1. Index
            lock (array)
            {
                AutoAI.ZIndexInstance++;
                this.ZIndex = AutoAI.ZIndexInstance;
            }

            if (this.ZIndex + 1 >= array.ZIndex()) return;
            array.Copy(this.ZIndex, parentZIndex);

            // 2. History
            this.History = history;
            switch ((HistoryAction)history.Distance)
            {
                case HistoryAction.Capture:
                    array[this.ZIndex, history.Y1, history.X1] = ChessType.Deaded;
                    array[this.ZIndex, history.Y2, history.X2] = array[parentZIndex, history.Y1, history.X1];
                    break;
                default:
                    break;
            }

            this.LevelSquared = array.GetLevelSquared(this.ZIndex);
        }

        protected abstract int DefaultValue();
        protected abstract bool EqualsValue(int thanDefault, int amout);
        protected abstract void CreateHistory(ChessType[,,] array, int zIndex);

        private int GetValueForce()
        {
            if (base.Count is 0) return this.LevelSquared;

            int defaultValue = this.DefaultValue();
            bool find = false;

            foreach (AutoAI item in this)
            {
                if (item.History == default) continue;
                if (item.History == History.Noway) continue;

                int value = item.GetValueForce();

                if (this.EqualsValue(defaultValue, value))
                {
                    defaultValue = value;
                    find = true;
                }
            }

            return find ? defaultValue : this.LevelSquared;
        }

        public override string ToString() => $"{this.ZIndex:000} V:{this.GetValueForce()} L:{this.LevelSquared} {this.History}";

        public void Dispose()
        {
            foreach (AutoAI item in this)
            {
                item.Dispose();
            }
            base.Clear();
        }
    }
}