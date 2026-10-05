using clipper.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Application = System.Windows.Application;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;
using Separator = System.Windows.Controls.Separator;

namespace clipper
{
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer appWatcherTimer;
        private readonly AppReactionService reactionService;

        private readonly ClippyStateService stateService;

        private readonly TrayService trayService;

        private ActiveWindowInfo? lastExternalWindow;

        private readonly BlinkService blinkService;
        private readonly SleepService sleepService;

        private string lastWindowSignature = "";

        private bool reallyExit = false;

        private readonly ReactionCooldownService cooldownService;

        public MainWindow()
        {
            InitializeComponent();

            trayService = new TrayService();

            trayService.ShowRequested += ShowFromTray;
            trayService.ExitRequested += ExitFromTray;

            sleepService = new SleepService();

            sleepService.FellAsleep += OnClippyFellAsleep;
            sleepService.WokeUp += OnClippyWokeUp;
            stateService = new ClippyStateService();

            reactionService = new AppReactionService();

            cooldownService = new ReactionCooldownService();

            blinkService = new BlinkService(ClippyImage);

            appWatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };

            appWatcherTimer.Tick += AppWatcherTimer_Tick;
        }

        private void Window_Loaded(object sender,  RoutedEventArgs e)
        {
            MoveToBottomRight();

            AnimationService.StartIdle(
                ClippyTranslate,
                ClippyRotate,
                ClippyScale);

            blinkService.Start();
            sleepService.Start();
            appWatcherTimer.Start();

            
        }

        private void ShowFromTray()
        {
            Show();
            WindowState = WindowState.Normal;
            Activate();

            MoveToBottomRight();
        }

   
        private void MoveToBottomRight()
        {
            var workArea = SystemParameters.WorkArea;

            Left = workArea.Right - Width - 20;
            Top = workArea.Bottom - Height - 20;
        }

        private void Say(string text)
        {
            if (stateService.IsSleeping)
                return;

            stateService.SetSpeaking();

            SpeechText.Text = text;

            AnimationService.React(ClippyTranslate);
            AnimationService.AnimateSpeechBubble(SpeechBubble);

            stateService.SetIdle();
        }

        private void OnClippyFellAsleep()
        {
            stateService.SetSleeping();
            blinkService.Stop();

            ClippyImage.Source = ImageService.Load("clipper_sleep.png");

            SpeechText.Text = "Zzz...";
        }
        private void OnClippyWokeUp()
        {
            stateService.SetWakingUp();
            ClippyImage.Source = ImageService.Load("clipper_original.png");

            blinkService.Start();

            Say("А? Я не спал.");
        }

        private void Window_Closed(object? sender,  EventArgs e)
        {
            blinkService.Stop();
            appWatcherTimer.Stop();
            sleepService.Stop();

        }

        private void Window_Closing(object? sender,  System.ComponentModel.CancelEventArgs e)
        {
            if (reallyExit)
                return;

            e.Cancel = true;

            Hide();
        }

        private void ExitFromTray()
        {
            reallyExit = true;

            blinkService.Stop();
            sleepService.Stop();
            appWatcherTimer.Stop();

            trayService.Dispose();

            Application.Current.Shutdown();
        }

        private void Window_MouseLeftButtonDown(
            object sender,
            MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (e.ClickCount == 2)
            {
                Say(reactionService.GetGenericPhrase());
                return;
            }

            DragMove();
        }


        private void AppWatcherTimer_Tick(object? sender, EventArgs e)
        {
            if (!stateService.CanReact)
                return;

            ActiveWindowInfo window = ActiveWindowService.GetActiveWindow();

            if (!ActiveWindowService.IsClipper(window))
            {
                lastExternalWindow = window;
            }

            string signature =
                $"{window.ProcessId}|{window.Title}";

            if (signature == lastWindowSignature)
                return;

            lastWindowSignature = signature;

            string cooldownKey = window.ProcessName;

            TimeSpan cooldown = window.ProcessName.ToLowerInvariant() switch
            {
                "firefox" => TimeSpan.FromMinutes(3),
                "devenv" => TimeSpan.FromSeconds(45),
                "unity" => TimeSpan.FromSeconds(45),
                "telegram" => TimeSpan.FromMinutes(1),
                "qbittorrent" => TimeSpan.FromMinutes(2),
                _ => TimeSpan.FromSeconds(30)
            };

            if (!cooldownService.CanReact(cooldownKey, cooldown))
                return;

            string? phrase =
                reactionService.TryGetReaction(window);

            if (!string.IsNullOrWhiteSpace(phrase))
            {
                Say(phrase);
                cooldownService.MarkReacted(cooldownKey);
            }
        }

        private void Window_MouseRightButtonUp(
            object sender,
            MouseButtonEventArgs e)
        {
            ContextMenu menu = new ContextMenu();

            MenuItem phraseItem = new MenuItem
            {
                Header = "Сказать что-нибудь"
            };

            phraseItem.Click += (_, _) =>
            {
                Say(reactionService.GetGenericPhrase());
            };

            MenuItem currentWindowItem = new MenuItem
            {
                Header = "Что я сейчас вижу?"
            };

            currentWindowItem.Click += (_, _) =>
            {
                if (lastExternalWindow == null)
                {
                    Say("Я пока ничего не успел подсмотреть.");
                    return;
                }

                Say(
                    $"{lastExternalWindow.ProcessName}\n" +
                    $"{lastExternalWindow.Title}"
                );
            };

            MenuItem resetItem = new MenuItem
            {
                Header = "Вернуться в угол"
            };

            resetItem.Click += (_, _) =>
            {
                MoveToBottomRight();
            };

            MenuItem exitItem = new MenuItem
            {
                Header = "Убить Скрепыша"
            };

            exitItem.Click += (_, _) =>
            {
                ExitFromTray();
            };

            MenuItem hideItem = new MenuItem
            {
                Header = "Спрятаться в трей"
            };

            hideItem.Click += (_, _) =>
            {
                Hide();
            };

            menu.Items.Add(phraseItem);
            menu.Items.Add(currentWindowItem);
            menu.Items.Add(resetItem);
            menu.Items.Add(hideItem);
            menu.Items.Add(new Separator());
            menu.Items.Add(exitItem);

            menu.IsOpen = true;
        }
    }
}