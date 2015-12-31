using System;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class SettingsTests
    {
        [Fact]
        public void CanGetExistingStringSetting()
        {
            var sut = new Settings(new TestSettingsSource(), new TestSettingParser());

            var setting = sut.GetSetting<string>("ExistingStringKey");
            Assert.Equal("ExistingStringValue", setting);
        }

        [Fact]
        public void CanGetParseableIntSetting()
        {
            var sut = new Settings(new TestSettingsSource(), new TestSettingParser());

            var setting = sut.GetSetting<int>("ExistingIntKey");
            Assert.Equal(7, setting);
        }

        [Fact]
        public void NotExistingStringSettingThrowsMissingKeyException()
        {
            var sut = new Settings(new TestSettingsSource(), new TestSettingParser());

            var exception = Assert.Throws<MissingKeyException>(
                () => sut.GetSetting<string>("NonExistingStringKey"));

            Assert.Equal("NonExistingStringKey", exception.Key);
        }

        [Fact]
        public void UnparseableIntSettingThrowsMalformedValueException()
        {
            var sut = new Settings(new TestSettingsSource(), new TestSettingParser());

            Assert.Throws<MalformedValueException>(
                () => sut.GetSetting<int>("MalformedIntKey"));
        }
    }

    internal class TestSettingsSource : IRawSettingsSource
    {
        public bool TryGetRawSetting(string key, out string value)
        {
            if (key == "ExistingStringKey")
            {
                value = "ExistingStringValue";
                return true;
            }

            if (key == "ExistingIntKey")
            {
                value = "7";
                return true;
            }

            if (key == "MalformedIntKey")
            {
                value = "this is not an int";
                return true;
            }

            value = null;
            return false;
        }
    }

    internal class TestSettingParser : IRawSettingParser
    {
        public Result<T> Parse<T>(string rawSettingValue)
        {
            if (typeof (T) == typeof (string))
            {
                // T is string, so it's safe to cast from string to object to T
                return Result.FromValue((T)(object)rawSettingValue);
            }

            if (typeof (T) == typeof (int))
            {
                int result;
                return int.TryParse(rawSettingValue, out result)
                    ? Result.FromValue((T) (object) result)
                    : Result<T>.Failure;
            }

            return Result<T>.Failure;
        }
    }
}
