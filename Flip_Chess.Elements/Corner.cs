namespace Flip_Chess.Elements
{
    internal readonly struct Corner
    {
        public readonly int X;
        public readonly int Y;

        public int L => X - 8;
        public int T => Y - 8;
        public int R => X + 8;
        public int B => Y + 8;

        public int LL => X - 28;
        public int TT => Y - 28;
        public int RR => X + 28;
        public int BB => Y + 28;

        //@Construct
        public Corner(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}