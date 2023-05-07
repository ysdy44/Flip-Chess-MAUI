namespace Flip_Chess.Chesses.Extensions
{
    public static partial class ChessExtensions
    {
        public static int GetLevelAbs(this ChessType type) => (int)type / 2;
        public static int GetLevelSquared(this ChessType type)
        {
            int c = (int)type;
            int h = c / 2;
            int s = h * h;
            return c % 2 == 0 ? s : -s;
        }
    }
}