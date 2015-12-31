using Confetti;

namespace ConfettiTests
{
    internal class TestSettingParser : IRawSettingParser
    {
        public Result<T> Parse<T>(string rawSettingValue)
        {
            if (typeof (T) == typeof (string))
            {
                // T is string, so it's safe to cast from string to object to T
                return Result.FromValue((T) (object) rawSettingValue);
            }

            if (typeof (T) == typeof (int))
            {
                int result;
                return int.TryParse(rawSettingValue, out result)
                    // T is int, so it's safe to cast from result (int) to object to T
                    ? Result.FromValue((T) (object) result)
                    : Result<T>.Failure;
            }

            return Result<T>.Failure;
        }
    }
}