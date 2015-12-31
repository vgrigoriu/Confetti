namespace Confetti
{
    public interface IRawSettingParser
    {
        Result<T> Parse<T>(string rawSettingValue);
    }
}