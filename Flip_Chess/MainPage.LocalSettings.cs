using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Microsoft.Maui.Storage;

namespace Flip_Chess
{
    partial class MainPage
    {

        // "Red" bool.False
        // "Black" bool.True
        // "Mode" int.0
        // "State" int.0
        // "Step" int.0
        // "Collection" int[]
        // "Random" int[]

        public bool SettingsRed
        {
            get => Preferences.Default.ContainsKey("Red") ? Preferences.Default.Get("Red", false) : false; // IsRed is false
            set => Preferences.Default.Set("Red", value);
        }

        public bool SettingsBlack
        {
            get => Preferences.Default.ContainsKey("Black") ? Preferences.Default.Get("Black", true) : true; // IsBlack is true
            set => Preferences.Default.Set("Black", value);
        }

        public GameMode SettingsMode
        {
            get => (GameMode)(Preferences.Default.ContainsKey("Mode") ? Preferences.Default.Get("Mode", 0) : 0);
            set => Preferences.Default.Set("Mode", (int)value);
        }

        public GameState SettingsState
        {
            get => (GameState)(Preferences.Default.ContainsKey("State") ? Preferences.Default.Get("State", 0) : 0);
            set => Preferences.Default.Set("State", (int)value);
        }

        public int SettingsStep
        {
            get => Preferences.Default.ContainsKey("Step") ? Preferences.Default.Get("Step", 0) : 0;
            set => Preferences.Default.Set("Step", value);
        }

        public bool ReadCollection()
        {
            return false;
            int h = this.Collection.Height();
            int w = this.Collection.Width();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int i = w.IndexOf(y, x);
                    if (Preferences.Default.ContainsKey($"Collection{i}"))
                    {
                        int item = Preferences.Default.Get($"Collection{i}", 0);
                        this.Collection[0, y, x] = (ChessType)item;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void WriteCollection()
        {
            int h = this.Collection.Height();
            int w = this.Collection.Width();

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int i = w.IndexOf(y, x);
                    int item = (int)this.Collection[0, y, x];
                    Preferences.Default.Set($"Collection{i}", item);
                }
            }
        }

        public bool ReadRandom()
        {
            return false;
            for (int i = 0; i < this.Randoms.Length; i++)
            {
                if (Preferences.Default.ContainsKey($"Random{i}"))
                {
                    int item = Preferences.Default.Get($"Random{i}", 0);
                    this.Randoms[i].Type = (ChessType)item;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void WriteRandom()
        {
            for (int i = 0; i < this.Randoms.Length; i++)
            {
                int item = (int)this.Randoms[i].Type;
                Preferences.Default.Set($"Random{i}", item);
            }
        }
    }
}