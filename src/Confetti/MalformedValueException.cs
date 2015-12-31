using System;

namespace Confetti
{
    public class MalformedValueException : Exception
    {
        public string Key { get; }

        public string Value { get; }

        public Type ValueType { get; }

        public MalformedValueException(string key, string value, Type valueType)
            : base($"Could not parse value '{value}' of key '{key}' as {valueType}")
        {
            Key = key;
            Value = value;
            ValueType = valueType;
        }
    }
}
