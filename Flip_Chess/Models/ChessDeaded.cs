using Flip_Chess.Chesses;
using System.ComponentModel;

namespace Flip_Chess.Models;

public sealed class ChessDeaded : IChess, INotifyPropertyChanged
{
    public double Opacity => System.Math.Clamp(this.Count, 0, 1);
    public bool Visibility => this.Count > 1;

    public int Count
    {
        get => this.count;
        set
        {
            if (this.count == value) return;
            this.count = value;
            this.OnPropertyChanged(nameof(Opacity)); // Notify 
            this.OnPropertyChanged(nameof(Visibility)); // Notify 
            this.OnPropertyChanged(nameof(Count)); // Notify 
        }
    }
    private int count;

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

    //@Construct
    public ChessDeaded(ChessType type) => this.type = type;

    public string ImageSource
    {
        get
        {
            switch (this.Type)
            {
                case ChessType.Unkonw:
                    return "item.png";
                case ChessType.Deaded:
                    return "none.png";
                case ChessType.RedSoldier:
                    return "red_soldier.png";
                case ChessType.BlackSoldier:
                    return "black_soldier.png";
                case ChessType.RedCannons:
                    return "red_cannons.png";
                case ChessType.BlackCannons:
                    return "black_cannons.png";
                case ChessType.RedKnight:
                    return "red_knight.png";
                case ChessType.BlackKnight:
                    return "black_knight.png";
                case ChessType.RedRook:
                    return "red_rook.png";
                case ChessType.BlackRook:
                    return "black_rook.png";
                case ChessType.RedElephant:
                    return "red_elephant.png";
                case ChessType.BlackElephant:
                    return "black_elephant.png";
                case ChessType.RedMandarins:
                    return "red_mandarins.png";
                case ChessType.BlackMandarins:
                    return "black_mandarins.png";
                case ChessType.RedKing:
                    return "red_king.png";
                case ChessType.BlackKing:
                    return "black_king.png";
                default:
                    return "none.png";
            }
        }
    }

    public override string ToString() => $"{this.Type} {this.Count}";

    //@Notify 
    /// <summary> Multicast event for property change notifications.</summary>
    public event PropertyChangedEventHandler PropertyChanged;
    /// <summary>
    /// Notifies listeners that a property value has changed.
    /// </summary>
    /// <param name="propertyName"> Name of the property used to notify listeners.</param>
    private void OnPropertyChanged(string propertyName) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}