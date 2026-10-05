using System;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

using Image = System.Windows.Controls.Image;

namespace clipper.Services
{
    public class BlinkService
    {
        private readonly Image image;
        private readonly BitmapImage normalImage;
        private readonly BitmapImage blinkImage;

        private readonly DispatcherTimer blinkTimer;
        private readonly DispatcherTimer reopenTimer;

        private readonly Random random = new();

        public BlinkService(Image image)
        {
            this.image = image;

            normalImage = LoadImage("clipper_original.png");
            blinkImage = LoadImage("clipper_blink.png");

            blinkTimer = new DispatcherTimer();
            blinkTimer.Tick += BlinkTimer_Tick;

            reopenTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(140)
            };

            reopenTimer.Tick += ReopenTimer_Tick;
        }

        private static BitmapImage LoadImage(string fileName)
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "Assets",
                fileName
            );

            BitmapImage bitmap = new();

            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            bitmap.Freeze();

            return bitmap;
        }

        public void Start()
        {
            ScheduleNextBlink();
        }

        public void Stop()
        {
            blinkTimer.Stop();
            reopenTimer.Stop();
        }

        private void ScheduleNextBlink()
        {
            blinkTimer.Stop();

            double seconds = random.NextDouble() * 5 + 4;

            blinkTimer.Interval =
                TimeSpan.FromSeconds(seconds);

            blinkTimer.Start();
        }

        private void BlinkTimer_Tick(
            object? sender,
            EventArgs e)
        {
            blinkTimer.Stop();

            image.Source = blinkImage;

            reopenTimer.Start();
        }

        private void ReopenTimer_Tick(
            object? sender,
            EventArgs e)
        {
            reopenTimer.Stop();

            image.Source = normalImage;

            ScheduleNextBlink();
        }
    }
}