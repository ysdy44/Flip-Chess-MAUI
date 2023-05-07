using Flip_Chess.Chesses;
using Flip_Chess.Chesses.AutoAIs;
using Flip_Chess.Chesses.Extensions;
using Flip_Chess.TestApp.Models;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Timers;

namespace Flip_Chess.TestApp
{
    public partial class MainPage : ContentPage
    {
        readonly Timer Timer = new Timer
        {
            Interval = 1000
        };

        // History
        public int Step => this.Historian.Count;
        public IList<History> Historian { get; } = new List<History>();

        // Collection
        public ChessType[,,] Collection { get; } = new ChessType[1024, 8, 4]; // Sertings
        public Chess[] Chesses { get; } = new Chess[8 * 4]
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
        public Chess[] Randoms { get; } = new Chess[8 * 4] // Sertings
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

        //@Construct
        public MainPage()
        {
            this.InitializeComponent();
            base.BindingContext = this;
            BindableLayout.SetItemsSource(this.ItemsControl, this.Chesses);

            this.Randoms.Home();
            this.Randoms.Random();

            this.Timer.Elapsed += async (s, e) =>
            {
                if (this.Step.IsRed())
                {
                    this.Timer.Stop();
                    this.ItemClick(await System.Threading.Tasks.Task.Run(this.FindRed));
                    this.Timer.Start();
                }
                else if (this.Step.IsBlack())
                {
                    this.Timer.Stop();
                    this.ItemClick(await System.Threading.Tasks.Task.Run(this.FindBlack));
                    this.Timer.Start();
                }
            };
            this.Timer.Start();
        }

        // AutoAi
        private History FindRed() => new RedAutoAICollection(this.Collection).FindAutoAI(this.Collection);
        private History FindBlack() => new BlackAutoAICollection(this.Collection).FindAutoAI(this.Collection);
    }
}