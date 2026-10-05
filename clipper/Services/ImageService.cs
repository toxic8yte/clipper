using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace clipper.Services
{
    public static class ImageService
    {
        public static BitmapImage Load(string fileName)
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
    }
}