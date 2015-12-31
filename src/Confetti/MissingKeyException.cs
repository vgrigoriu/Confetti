using System;

namespace Confetti
{
    public class MissingKeyException : Exception
    {
        public string Key { get; }

        public MissingKeyException(string key)
            : base($"Key '{key}' was not found")
        {
            Key = key;
        }
    }
}