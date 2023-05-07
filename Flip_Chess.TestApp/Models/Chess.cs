using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Microsoft.Maui.Graphics;
using System.ComponentModel;

namespace Flip_Chess.TestApp.Models
{
    public sealed class Chess : IChess, INotifyPropertyChanged
    {
        public Color Color
        {
            get
            {
                if (this.Type.IsRed()) return Colors.DarkRed;
                if (this.Type.IsBlack()) return Colors.Black;
                return Colors.Peru;
            }
        }
        public ChessText Text => (ChessText)this.Type;
        public bool Visibility => this.Type != ChessType.Deaded;

        public ChessType Type
        {
            get => this.type;
            set
            {
                if (this.type == value) return;
                this.type = value;
                this.OnPropertyChanged(nameof(Color)); // Notify 
                this.OnPropertyChanged(nameof(Text)); // Notify 
                this.OnPropertyChanged(nameof(Visibility)); // Notify 
                this.OnPropertyChanged(nameof(Type)); // Notify 
            }
        }
        private ChessType type;

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