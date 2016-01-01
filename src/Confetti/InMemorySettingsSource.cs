using System.Collections.Generic;

namespace Confetti
{
    public class InMemorySettingsSource : IRawSettingsSource
    {
        private readonly IReadOnlyDictionary<string, string> settings;

        public InMemorySettingsSource(IReadOnlyDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        public bool TryGetRawSetting(string key, out string value)
        {
            return settings.TryGetValue(key, out value);
        }
    }
}
