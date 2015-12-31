namespace Confetti
{
    public interface IRawSettingsSource
    {
        bool TryGetRawSetting(string key, out string value);
    }
}