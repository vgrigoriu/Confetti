using System;

namespace Confetti
{
    public class Settings
    {
        private readonly IRawSettingsSource source;
        private readonly IRawSettingParser parser;

        public Settings(IRawSettingsSource source, IRawSettingParser parser)
        {
            this.source = source;
            this.parser = parser;
        }

        /// <summary>
        /// Gets the setting named `key`, parsed as a value of type `T`
        /// </summary>
        /// <typeparam name="T">The type of the value to return</typeparam>
        /// <param name="key">The name of the setting key</param>
        /// <returns>The setting named `key`, parsed as a value of type `T`</returns>
        public T GetSetting<T>(string key)
        {
            string rawValue;
            if (source.TryGetRawSetting(key, out rawValue))
            {
                return parser.TryParse<T>(rawValue).Value;
            }

            throw new MissingKeyException();
        }
    }

    public interface IRawSettingsSource
    {
        bool TryGetRawSetting(string key, out string value);
    }

    public static class Result
    {
        public static Result<T> FromValue<T>(T value)
        {
            return new Result<T>(value);
        }
    }

    public class Result<T>
    {
        public static Result<T> Failure { get; } = new Result<T>();

        // a failure
        private Result()
        {
            isFailed = true;
        }

        internal Result(T value)
        {
            this.value = value;
        }

        private readonly T value;

        public T Value
        {
            get
            {
                if (isFailed)
                {
                    throw new InvalidOperationException("Cannot get value of failed result");
                }

                return value;
            }
        }

        private readonly bool isFailed;

        public bool IsFailed => isFailed;
    }

    public interface IRawSettingParser
    {
        Result<T> TryParse<T>(string rawSettingValue);
    }

    public class MissingKeyException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public MissingKeyException()
        {
        }

        public MissingKeyException(string message) : base(message)
        {
        }

        public MissingKeyException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
