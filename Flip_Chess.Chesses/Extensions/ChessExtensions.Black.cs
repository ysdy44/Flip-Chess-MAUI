namespace Flip_Chess.Chesses.Extensions
{
    partial class ChessExtensions
    {
        public static ChessType ToBlack(this ChessCount count) => (ChessType)(1 + (int)count / 5);
        public static bool IsBlack(this int step) => step % 2 != 0; // Index
        public static bool IsBlack(this ChessType type)
        {
            int c = (int)type;
            return c > 1 && c % 2 != 0;
        }

        public static HistoryRelation BlackRelateTo(this ChessType previous, ChessType next)
        {
            switch (next)
            {
                case ChessType.Unkonw:
                    return HistoryRelation.Unkonw;

                case ChessType.Deaded:
                    return HistoryRelation.WeakEnemy;

                case ChessType.BlackSoldier:
                case ChessType.BlackCannons:
                case ChessType.BlackKnight:
                case ChessType.BlackRook:
                case ChessType.BlackElephant:
                case ChessType.BlackMandarins:
                case ChessType.BlackKing:
                    return HistoryRelation.Friend;

                case ChessType.RedSoldier:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedCannons:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedKnight:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedRook:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedElephant:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedMandarins:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.RedKing:
                    switch (previous)
                    {
                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.Friend;

                        case ChessType.BlackSoldier:
                        case ChessType.BlackKing:
                            return HistoryRelation.WeakEnemy;

                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                default: return HistoryRelation.StrongEnemy;
            }
        }
    }
}