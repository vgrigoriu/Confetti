using System.Collections.Generic;
using Confetti;

namespace ConfettiTests
{
    internal class TestSettingsSource : IRawSettingsSource
    {
        private readonly IDictionary<string, string> settings;

        public TestSettingsSource(IDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        public bool TryGetRawSetting(string key, out string value)
        {
            return settings.TryGetValue(key, out value);
        }
    }
}