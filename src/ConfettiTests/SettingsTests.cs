using System.Collections.Generic;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class SettingsTests
    {
        private const string ExistingStringKey = "ExistingStringKey";
        private const string ExistingIntKey = "ExistingIntKey";
        private const string MalformedIntKey = "MalformedIntKey";

        private readonly IRawSettingsSource settingsSource = new TestSettingsSource(
            new Dictionary<string, string>
            {
                {ExistingStringKey, "ExistingStringValue"},
                {ExistingIntKey, "7"},
                {MalformedIntKey, "this is not an int"}
            });

        [Fact]
        public void CanGetExistingStringSetting()
        {
            var sut = new Settings(settingsSource, new TestSettingParser());

            var setting = sut.GetSetting<string>(ExistingStringKey);
            Assert.Equal("ExistingStringValue", setting);
        }

        [Fact]
        public void CanGetParseableIntSetting()
        {
            var sut = new Settings(settingsSource, new TestSettingParser());

            var setting = sut.GetSetting<int>(ExistingIntKey);
            Assert.Equal(7, setting);
        }

        [Fact]
        public void NotExistingStringSettingThrowsMissingKeyException()
        {
            var sut = new Settings(settingsSource, new TestSettingParser());

            var exception = Assert.Throws<MissingKeyException>(
                () => sut.GetSetting<string>("NonExistingStringKey"));

            Assert.Equal("NonExistingStringKey", exception.Key);
        }

        [Fact]
        public void UnparseableIntSettingThrowsMalformedValueException()
        {
            var sut = new Settings(settingsSource, new TestSettingParser());

            var exception = Assert.Throws<MalformedValueException>(
                () => sut.GetSetting<int>(MalformedIntKey));

            Assert.Equal(MalformedIntKey, exception.Key);
            Assert.Equal("this is not an int", exception.Value);
            Assert.Equal(typeof(int), exception.ValueType);
        }
    }
}
