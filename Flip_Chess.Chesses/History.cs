namespace Flip_Chess.Chesses
{
    public struct History
    {
        public int Y1;
        public int X1;

        public int Y2;
        public int X2;

        public int Y => (this.Y1 + this.Y2) / 2;
        public int X => (this.X1 + this.X2) / 2;
        public HistoryDistance Distance
        {
            get
            {
                if (this == Noway)
                    return HistoryDistance.Noway;

                if (this.Y1 == -1)
                    return HistoryDistance.Others;
                if (this.X1 == -1)
                    return HistoryDistance.Others;
                if (this.Y2 == -1)
                    return HistoryDistance.Others;
                if (this.X2 == -1)
                    return HistoryDistance.Others;

                if (this.Y1 == this.Y2 && this.X1 == this.X2)
                    return HistoryDistance.DistanceIs0;
                if (System.Math.Abs(this.Y1 - this.Y2) + System.Math.Abs(this.X1 - this.X2) == 1)
                    return HistoryDistance.DistanceIs1;

                return HistoryDistance.Others;
            }
        }

        //@Construct
        public History(int y, int x) : this(y, x, y, x) { }

        public History(int y1, int x1, int y2, int x2) : this()
        {
            this.Y1 = y1;
            this.X1 = x1;

            this.Y2 = y2;
            this.X2 = x2;
        }

        //@Static
        public static readonly History Noway = new History(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);

        public static bool operator ==(History left, History right) => left.Equals(right);
        public static bool operator !=(History left, History right) => !left.Equals(right);

        public bool Equals(History other)
        {
            if (this.Y1 != other.Y1) return false;
            if (this.X1 != other.X1) return false;

            if (this.Y2 != other.Y2) return false;
            if (this.X2 != other.X2) return false;

            return true;
        }

        public override bool Equals(object obj) => base.Equals(obj);
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            switch (this.Distance)
            {
                case HistoryDistance.Noway:
                    return $"(≧∇≦)ﾉ";
                case HistoryDistance.DistanceIs0:
                    return $"(Y:{this.Y}, X:{this.X})";
                case HistoryDistance.DistanceIs1:
                    return $"(Y:{this.Y1}, X:{this.X1}) -> (Y:{this.Y2}, X:{this.X2})";
                default:
                    return string.Empty;
            }
        }
    }
}