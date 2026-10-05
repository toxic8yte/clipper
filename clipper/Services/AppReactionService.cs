using clipper.Data;
using clipper.Models;

namespace clipper.Services
{
    public class AppReactionService
    {
        private readonly Random random = new();

        public string? TryGetReaction(ActiveWindowInfo window)
        {
            if (string.IsNullOrWhiteSpace(window.Title) &&
                string.IsNullOrWhiteSpace(window.ProcessName))
            {
                return null;
            }

            // Сначала специальные приложения.
            // Порядок важен: Tor должен проверяться до обычного Firefox.
            foreach (AppReaction reaction in ReactionLibrary.AppReactions)
            {
                if (!Matches(reaction, window))
                    continue;

                if (random.NextDouble() > reaction.Probability)
                    return null;

                return GetRandom(reaction.Phrases);
            }

            // Затем универсальное правило для VPN
            if (LooksLikeVpn(window))
            {
                if (random.NextDouble() < 0.45)
                    return GetRandom(ReactionLibrary.VpnPhrases);
            }

            return null;
        }

        public string GetGenericPhrase()
        {
            return GetRandom(ReactionLibrary.GenericPhrases);
        }

        private bool Matches(
            AppReaction reaction,
            ActiveWindowInfo window)
        {
            bool hasProcessRules = reaction.ProcessNames.Length > 0;
            bool hasTitleRules = reaction.TitleKeywords.Length > 0;
            bool hasPathRules = reaction.PathKeywords.Length > 0;

            bool processMatches =
                !hasProcessRules ||
                reaction.ProcessNames.Any(
                    name => window.ProcessName.Equals(
                        name,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            bool titleMatches =
                !hasTitleRules ||
                reaction.TitleKeywords.Any(
                    keyword => window.Title.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            bool pathMatches =
                !hasPathRules ||
                reaction.PathKeywords.Any(
                    keyword => window.ProcessPath.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase
                    )
                );

            return processMatches &&
                   titleMatches &&
                   pathMatches;
        }

        private bool LooksLikeVpn(ActiveWindowInfo window)
        {
            foreach (string keyword in ReactionLibrary.VpnKeywords)
            {
                if (window.Title.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    window.ProcessName.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase) ||
                    window.ProcessPath.Contains(
                        keyword,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private string GetRandom(string[] phrases)
        {
            if (phrases.Length == 0)
                return "";

            return phrases[random.Next(phrases.Length)];
        }
    }
}