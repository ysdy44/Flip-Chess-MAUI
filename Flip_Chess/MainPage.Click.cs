using Flip_Chess.Chesses;
using Flip_Chess.Chesses.Extensions;
using Microsoft.Maui.Dispatching;
using System;
using System.Windows.Input;

namespace Flip_Chess
{
    partial class MainPage
    {

        //@Delegate
        public event EventHandler CanExecuteChanged;

        //@Command
        public ICommand Command => this;
        public bool CanExecute(object parameter) => parameter != default;
        public void Execute(object parameter)
        {
            if (parameter is OptionType type)
            {
                this.Click(type);
            }

            if (parameter is IChess chess)
            {
                this.Click(chess);
            }
        }

        public async void Click()
        {
            switch (this.State)
            {
                case GameState.None:
                    if (this.Step.IsRed())
                    {
                        if (this.IsRedComputer)
                        {
                            this.Timer.Stop();
                            History history = await System.Threading.Tasks.Task.Run(this.FindRed);
                            await base.Dispatcher.DispatchAsync(() => this.ItemClick(history));
                            this.Timer.Start();
                        }
                    }
                    else if (this.Step.IsBlack())
                    {
                        if (this.IsBlackComputer)
                        {
                            this.Timer.Stop();
                            History history = await System.Threading.Tasks.Task.Run(this.FindBlack);
                            await base.Dispatcher.DispatchAsync(() => this.ItemClick(history));
                            this.Timer.Start();
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void Click(IChess chess)
        {
            switch (this.State)
            {
                case GameState.None:
                    if (this.Step.IsRed())
                    {
                        if (this.IsRedComputer) break;
                        this.ItemClick(chess);
                    }
                    else if (this.Step.IsBlack())
                    {
                        if (this.IsBlackComputer) break;
                        this.ItemClick(chess);
                    }
                    break;
                default:
                    break;
            }
        }

        public void Click(OptionType type)
        {
            switch (type)
            {
                case OptionType.Ai:
                    if (this.Step.IsBlack()) this.ItemClick(this.FindRed());
                    else if (this.Step.IsRed()) this.ItemClick(this.FindBlack());
                    break;
                case OptionType.Play:
                    this.State = GameState.None;
                    this.SettingsState = GameState.None; // Sertings
                    break;

                case OptionType.Lose:
                    this.SelectedIndex = 1;
                    this.State = GameState.Lose;
                    this.SettingsState = GameState.Lose; // Sertings
                    break;
                case OptionType.Win:
                    this.SelectedIndex = 1;
                    this.State = GameState.Win;
                    this.SettingsState = GameState.Win; // Sertings
                    break;
                case OptionType.Pause:
                    this.SelectedIndex = 1;
                    this.State = GameState.Pause;
                    this.SettingsState = GameState.Pause; // Sertings
                    break;

                case OptionType.Continue:
                    this.State = GameState.None;
                    this.SettingsState = GameState.None; // Sertings
                    break;
                case OptionType.Restart:
                    this.HistorianCount = 0; // 1. Clear HistorianCount
                    this.Historian.Clear(); // 2. Clear Historian
                    this.SettingsStep = this.Step; // 3. Save HistorianCount + Historian // Sertings

                    //this.Randoms.Home();
                    //this.Randoms.Random();
                    //this.WriteRandom(); // Sertings

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

                    // Storyboard
                    if (this.CanClip)
                        this.BeginClip();
                    else
                        this.Click(OptionType.UIClipCompleted);

                    this.Relive();

                    this.State = GameState.None;
                    this.SettingsState = GameState.None; // Sertings

                    this.HideFlip();
                    this.StopFlip();

                    this.HideCapture();
                    this.StopCapture();

                    this.HideCemetery();
                    this.StopCemetery();
                    break;
                case OptionType.Next:
                    this.Randoms.Home();
                    this.Randoms.Random();
                    this.WriteRandom(); // Sertings

                    this.Click(OptionType.Restart);
                    break;

                case OptionType.FullScreen:
                    this.ClickFullScreen();
                    break;
                case OptionType.UnFullScreen:
                    this.ClickUnFullScreen();
                    break;

                case OptionType.About:
                    this.SelectedIndex = 0;
                    break;
                case OptionType.Tutorial:
                    this.SelectedIndex = 2;
                    break;

                case OptionType.Step1:
                    this.SelectedIndex = 3;
                    break;
                case OptionType.Step2:
                    this.SelectedIndex = 4;
                    break;
                case OptionType.Step3:
                    this.SelectedIndex = 5;
                    break;
                case OptionType.Step4:
                    this.State = GameState.None;
                    this.SettingsState = GameState.None; // Sertings
                    break;

                case OptionType.UIRedChanged:
                    this.SettingsRed = this.IsRedComputer; // Settings
                    break;
                case OptionType.UIBlackChanged:
                    this.SettingsBlack = this.IsBlackComputer; // Settings
                    break;
                case OptionType.UIModeChanged:
                    this.SettingsMode = this.Mode; // Settings
                    this.Click(OptionType.Restart);
                    break;

                case OptionType.UIFlipCompleted: /// <see cref="OptionType.UIFlipCompleted"/>
                    this.Shown();
                    break;
                case OptionType.UICaptureCompleted: /// <see cref="OptionType.UICaptureCompleted"/>
                    this.Shown();

                    // Storyboard
                    if (this.CanCemetery)
                        this.BeginCemetery();
                    else
                        this.Click(OptionType.UICemeteryCompleted);
                    break;
                case OptionType.UICemeteryCompleted: /// <see cref="OptionType.UICemeteryCompleted"/>
                    this.HideCapture();

                    this.Deaded();
                    break;
                case OptionType.UIClipCompleted: /// <see cref="OptionType.UIClipCompleted"/>
                    this.Shown();
                    break;

                default:
                    break;
            }
        }
    }
}