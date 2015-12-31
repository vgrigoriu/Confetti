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
        public void NotExistingStringSettingThrowsException()
        {
            var sut = new Settings(new TestSettingsSource(), new TestSettingParser());

            Assert.Throws<MissingKeyException>(() => sut.GetSetting<string>("NonExistingStringKey"));
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
                return Result.FromValue((T)(object)rawSettingValue);
            }

            return Result<T>.Failure;
        }
    }
}
