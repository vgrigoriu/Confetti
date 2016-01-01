using System.Collections.Generic;

namespace Confetti
{
    public class MultipleSettingsSource : IRawSettingsSource
    {
        private readonly IReadOnlyCollection<IRawSettingsSource> settingsSources;

        public MultipleSettingsSource(IReadOnlyCollection<IRawSettingsSource> settingsSources)
        {
            this.settingsSources = settingsSources;
        }

        public bool TryGetRawSetting(string key, out string value)
        {
            foreach (var settingsSource in settingsSources)
            {
                if (settingsSource.TryGetRawSetting(key, out value))
                {
                    // found it!
                    return true;
                }
            }

            // no luck
            value = null;
            return false;
        }
    }
}
