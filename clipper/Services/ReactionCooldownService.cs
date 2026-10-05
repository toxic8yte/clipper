using System;
using System.Collections.Generic;

namespace clipper.Services
{
    public class ReactionCooldownService
    {
        private readonly Dictionary<string, DateTime> lastReactionTimes = new();

        private readonly TimeSpan defaultCooldown =
            TimeSpan.FromSeconds(30);

        public bool CanReact(string key)
        {
            return CanReact(key, defaultCooldown);
        }

        public bool CanReact(string key, TimeSpan cooldown)
        {
            if (!lastReactionTimes.TryGetValue(key, out DateTime lastTime))
                return true;

            return DateTime.Now - lastTime >= cooldown;
        }

        public void MarkReacted(string key)
        {
            lastReactionTimes[key] = DateTime.Now;
        }

        public void Reset(string key)
        {
            lastReactionTimes.Remove(key);
        }

        public void ResetAll()
        {
            lastReactionTimes.Clear();
        }
    }
}