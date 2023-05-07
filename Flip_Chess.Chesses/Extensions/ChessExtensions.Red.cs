namespace Flip_Chess.Chesses.Extensions
{
    partial class ChessExtensions
    {
        public static ChessType ToRed(this ChessCount count) => (ChessType)(0 + (int)count / 5);
        public static bool IsRed(this int step) => step % 2 == 0; // Index
        public static bool IsRed(this ChessType type)
        {
            int c = (int)type;
            return c > 1 && c % 2 == 0;
        }

        public static HistoryRelation RedRelateTo(this ChessType previous, ChessType next)
        {
            switch (next)
            {
                case ChessType.Unkonw:
                    return HistoryRelation.Unkonw;

                case ChessType.Deaded:
                    return HistoryRelation.WeakEnemy;

                case ChessType.RedSoldier:
                case ChessType.RedCannons:
                case ChessType.RedKnight:
                case ChessType.RedRook:
                case ChessType.RedElephant:
                case ChessType.RedMandarins:
                case ChessType.RedKing:
                    return HistoryRelation.Friend;

                case ChessType.BlackSoldier:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedSoldier:
                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackCannons:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedCannons:
                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackKnight:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedKnight:
                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackRook:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedRook:
                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackElephant:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedElephant:
                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackMandarins:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedMandarins:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;
                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                case ChessType.BlackKing:
                    switch (previous)
                    {
                        case ChessType.BlackSoldier:
                        case ChessType.BlackCannons:
                        case ChessType.BlackKnight:
                        case ChessType.BlackRook:
                        case ChessType.BlackElephant:
                        case ChessType.BlackMandarins:
                        case ChessType.BlackKing:
                            return HistoryRelation.Friend;

                        case ChessType.RedSoldier:
                        case ChessType.RedKing:
                            return HistoryRelation.WeakEnemy;

                        default:
                            return HistoryRelation.StrongEnemy;
                    }

                default: return HistoryRelation.StrongEnemy;
            }
        }
    }
}