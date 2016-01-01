using System;

namespace Confetti
{
    public class EnvironmentSettingsSource : IRawSettingsSource
    {
        public bool TryGetRawSetting(string key, out string value)
        {
            value = Environment.GetEnvironmentVariable(key);
            return value != null;
        }
    }
}