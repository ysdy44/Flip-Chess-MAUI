using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.Elements;
using Flip_Chess.Models;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;

namespace Flip_Chess
{
    internal sealed class LinesCanvas4x2 : LinesCanvas
    {
        public LinesCanvas4x2() : base(4, 2, 100) { }
    }

    internal sealed class MainLinesCanvas : LinesCanvas
    {
        public MainLinesCanvas() : base(App.Width, App.Height, 100) { }
    }

    internal sealed class ModeVisible
    {
        public bool IsVisible000 { get; set; }
        public bool IsVisible001 { get; set; }
        public bool IsVisible002 { get; set; }
        public bool IsVisible003 { get; set; }
        public bool IsVisible004 { get; set; }
        public bool IsVisible005 { get; set; }
    }

    internal sealed class Player
    {
        public string Icon { get; set; }
        public string Text { get; set; }
    }


    public sealed partial class MainPage : ContentPage, ICommand
    {
        //@Strings
        private int W => App.Width * 100;
        private int H => App.Height * 100;

        readonly Timer Timer = new Timer
        {
            Interval = 1000
        };

        // Previous
        public ChessType Previous { get; set; }
        public int PreviousY { get; set; } = -1;
        public int PreviousX { get; set; } = -1;

        // History
        public int HistorianCount { get; set; } // Sertings
        public int Step => this.HistorianCount + this.Historian.Count;
        public ObservableCollection<History> Historian { get; } = new ObservableCollection<History>();

        // Collection
        public ChessType[,,] Collection { get; } = new ChessType[1024 * 128, App.Height, App.Width]; // Sertings
        public Chess[] Randoms { get; } = new Chess[App.Height * App.Width] // Sertings
        {
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
            new Chess(), new Chess(), new Chess(), new Chess(),
        };
        public ChessAlive[] Chesses { get; } = new ChessAlive[App.Height * App.Width]
        {
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
            new ChessAlive(), new ChessAlive(), new ChessAlive(), new ChessAlive(),
        };

        // Storyboard
        public ChessDeaded[] BlackCemetery { get; } = new ChessDeaded[]
        {
            new ChessDeaded(ChessType.BlackKing),
            new ChessDeaded(ChessType.BlackMandarins),
            new ChessDeaded(ChessType.BlackElephant),
            new ChessDeaded(ChessType.BlackRook),
            new ChessDeaded(ChessType.BlackKnight),
            new ChessDeaded(ChessType.BlackCannons),
            new ChessDeaded(ChessType.BlackSoldier)
        };
        public ChessDeaded[] RedCemetery { get; } = new ChessDeaded[]
        {
            new ChessDeaded(ChessType.RedKing),
            new ChessDeaded(ChessType.RedMandarins),
            new ChessDeaded(ChessType.RedElephant),
            new ChessDeaded(ChessType.RedRook),
            new ChessDeaded(ChessType.RedKnight),
            new ChessDeaded(ChessType.RedCannons),
            new ChessDeaded(ChessType.RedSoldier)
        };

        //@Construct
        public MainPage()
        {
            this.InitializeComponent();
            base.BindingContext = this;
        
            this.IsRedComputer = this.SettingsRed; // Sertings
            this.IsBlackComputer = this.SettingsBlack; // Sertings
            this.Mode = this.SettingsMode; // Sertings
            this.State = this.SettingsState; // Sertings
            this.HistorianCount = this.SettingsStep; // Sertings

            if (this.ReadRandom() && this.ReadCollection()) // Sertings
            {
                this.Shown();
                this.Deaded();
            }
            else
            {
                this.Randoms.Home();
                this.Randoms.Random();
                this.WriteRandom(); // Sertings

                switch (this.Mode)
                {
                    case GameMode.None:
                        this.Collection.Clear();
                        break;
                    case GameMode.King:
                        this.Collection.Clear();
                        this.Collection.Copy(this.Randoms, ChessType.RedKing);
                        this.Collection.Copy(this.Randoms, ChessType.BlackKing);
                        break;
                    case GameMode.Half:
                        this.Collection.Clear();
                        this.Collection.CopyHalf(this.Randoms);
                        break;
                    case GameMode.All:
                        this.Collection.Copy(this.Randoms);
                        break;
                    default:
                        break;
                }
                this.WriteCollection(); // Sertings

                this.Shown();
                this.Relive();
            }

            this.Timer.Elapsed += (s, e) => this.Click();
            this.Timer.Start();

            // UI
            this.Text1 = this.Step.ToString();
            this.Text2 = this.Step.IsBlack() ? "黑方" : "红方";
            this.Historian.CollectionChanged += (s, e) =>
            {
                this.Text1 = this.Step.ToString();
                this.Text2 = this.Step.IsBlack() ? "黑方" : "红方";
            };

            base.SizeChanged += (s, e) =>
            {
                this.SetMargin((int)base.Width, (int)base.Height, 50);
            };
        }

        // AutoAi
        private History FindRed() => new RedAutoAICollection(this.Collection).FindAutoAI(this.Collection);
        private History FindBlack() => new BlackAutoAICollection(this.Collection).FindAutoAI(this.Collection);


        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            DisplayActionSheet(null, null, null, this.FlowDirection, new string[]
            {
                "1111",
                "222",
                "3333",
                "4444",
            });
        }

        private void RedButton_Clicked(object sender, System.EventArgs e)
        {
            this.IsRedComputer = !this.IsRedComputer;
            this.Click(OptionType.UIRedChanged);

            base.DisplayAlert(this.IsRedComputer.ToString(), "RedChanged", "OK");
        }

        private void BlackButton_Clicked(object sender, System.EventArgs e)
        {
            this.IsBlackComputer = !this.IsBlackComputer;
            this.Click(OptionType.UIBlackChanged);

            base.DisplayAlert(this.IsBlackComputer.ToString(), "BlackChanged", "OK");
        }
    }
}