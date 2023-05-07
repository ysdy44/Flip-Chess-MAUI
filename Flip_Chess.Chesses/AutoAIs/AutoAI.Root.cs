using Flip_Chess.Chesses.Extensions;

namespace Flip_Chess.Chesses.AutoAIs
{
    partial class AutoAI
    {
        protected AutoAI(ChessType[,,] array)
        {
            // 1. Index
            lock (array)
            {
                AutoAI.ZIndexInstance = 0;
                this.ZIndex = 0;
            }

            // 2. History
            this.LevelSquared = array.GetLevelSquared(0);

            // 3. Children
            this.CreateHistory(array, 0);

            // 1
            if (base.Count is 0) return;
            foreach (AutoAI item in this)
            {
                if (item.ZIndex + 1 >= array.ZIndex()) return;
                item.CreateHistory(array, item.ZIndex);
            }

            // 2
            foreach (AutoAI item in this)
            {
                if (item is null) return;
                if (item.Count is 0) return;
                foreach (AutoAI item2 in item)
                {
                    if (item2 is null) return;
                    if (item2.ZIndex + 1 >= array.ZIndex()) return;
                    item2.CreateHistory(array, item2.ZIndex);
                }
            }

            // 3
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                {
                    if (item2 is null) return;
                    if (item2.Count is 0) return;
                    foreach (AutoAI item3 in item2)
                    {
                        if (item3 is null) return;
                        if (item3.ZIndex + 1 >= array.ZIndex()) return;
                        item3.CreateHistory(array, item3.ZIndex);
                    }
                }

            // 4
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                    foreach (AutoAI item3 in item2)
                    {
                        if (item3 is null) return;
                        if (item3.Count is 0) return;
                        foreach (AutoAI item4 in item3)
                        {
                            if (item4 is null) return;
                            if (item4.ZIndex + 1 >= array.ZIndex()) return;
                            item4.CreateHistory(array, item4.ZIndex);
                        }
                    }

            // 5
            foreach (AutoAI item in this)
                foreach (AutoAI item2 in item)
                    foreach (AutoAI item3 in item2)
                        foreach (AutoAI item4 in item3)
                        {
                            if (item4 is null) return;
                            if (item4.Count is 0) return;
                            foreach (AutoAI item5 in item4)
                            {
                                if (item5 is null) return;
                                if (item5.ZIndex + 1 >= array.ZIndex()) return;
                                item5.CreateHistory(array, item5.ZIndex);
                            }
                        }
        }

        public History FindAutoAI(ChessType[,,] array)
        {
            if (base.Count is 0) return array.RandomFlip();

            int defaultValue = this.DefaultValue();
            AutoAI find = null;

            foreach (AutoAI item in this)
            {
                int value = item.GetValueForce();

                if (this.EqualsValue(defaultValue, value))
                {
                    defaultValue = value;
                    find = item;
                }
                else if (defaultValue == value)
                {
                    if (find != null)
                    {
                        if (this.EqualsValue(find.LevelSquared, item.LevelSquared))
                        {
                            defaultValue = value;
                            find = item;
                        }
                    }
                }
            }

            if (find != null)
            {
                if (find.History != History.Noway)
                {
                    return find.History;
                }
            }

            History flip = array.RandomFlip();
            if (flip != History.Noway) return flip;

            foreach (AutoAI item in this)
            {
                if (item.History != History.Noway)
                {
                    return item.History;
                }
            }

            return History.Noway;
        }
    }
}