using Flip_Chess.Chesses;
using System.ComponentModel;

namespace Flip_Chess.Models
{
    public sealed class ChessAlive : IChess, INotifyPropertyChanged
    {
        public bool Visibility
        {
            get => this.visibility;
            set
            {
                if (this.visibility == value) return;
                this.visibility = value;
                this.OnPropertyChanged(nameof(Visibility)); // Notify 
            }
        }
        private bool visibility;

        public ChessType Type
        {
            get => this.type;
            set
            {
                if (this.type == value) return;
                this.type = value;
                this.OnPropertyChanged(nameof(ImageSource)); // Notify 
                this.OnPropertyChanged(nameof(Type)); // Notify 
            }
        }
        private ChessType type;

        public string ImageSource
        {
            get
            {
                switch (this.Type)
                {
                    case ChessType.Unkonw:
                        return "item_x2.png";
                    case ChessType.Deaded:
                        return "none_x2.png";
                    case ChessType.RedSoldier:
                        return "red_soldier_x2.png";
                    case ChessType.BlackSoldier:
                        return "black_soldier_x2.png";
                    case ChessType.RedCannons:
                        return "red_cannons_x2.png";
                    case ChessType.BlackCannons:
                        return "black_cannons_x2.png";
                    case ChessType.RedKnight:
                        return "red_knight_x2.png";
                    case ChessType.BlackKnight:
                        return "black_knight_x2.png";
                    case ChessType.RedRook:
                        return "red_rook_x2.png";
                    case ChessType.BlackRook:
                        return "black_rook_x2.png";
                    case ChessType.RedElephant:
                        return "red_elephant_x2.png";
                    case ChessType.BlackElephant:
                        return "black_elephant_x2.png";
                    case ChessType.RedMandarins:
                        return "red_mandarins_x2.png";
                    case ChessType.BlackMandarins:
                        return "black_mandarins_x2.png";
                    case ChessType.RedKing:
                        return "red_king_x2.png";
                    case ChessType.BlackKing:
                        return "black_king_x2.png";
                    default:
                        return "none_x2.png";
                }
            }
        }

        public override string ToString() => this.Type.ToString();

        //@Notify 
        /// <summary> Multicast event for property change notifications.</summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName"> Name of the property used to notify listeners.</param>
        private void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}