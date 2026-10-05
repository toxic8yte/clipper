namespace clipper.Models
{
    public class AppReaction
    {
        public string Name { get; set; } = "";

        // Имя процесса без .exe
        // Например: devenv, Unity, qbittorrent
        public string[] ProcessNames { get; set; } = Array.Empty<string>();

        // Дополнительные слова в заголовке окна
        public string[] TitleKeywords { get; set; } = Array.Empty<string>();

        // Дополнительные слова в полном пути процесса
        // Особенно полезно для Tor Browser
        public string[] PathKeywords { get; set; } = Array.Empty<string>();

        public string[] Phrases { get; set; } = Array.Empty<string>();

        public double Probability { get; set; } = 0.35;
    }
}