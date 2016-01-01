using System;
using System.ComponentModel;

namespace Confetti
{
    public class DefaultSettingParser : IRawSettingParser
    {
        public Result<T> Parse<T>(string rawSettingValue)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof (T));
                var parsedValue = (T) converter.ConvertFromInvariantString(rawSettingValue);
                return Result.FromValue(parsedValue);
            }
            catch (Exception)
            {
                return Result<T>.Failure;
            }
        }
    }
}