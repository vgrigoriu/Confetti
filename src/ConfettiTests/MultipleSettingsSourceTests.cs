using System;
using System.Collections.Generic;
using Confetti;
using Xunit;

namespace ConfettiTests
{
    public class MultipleSettingsSourceTests
    {
        private readonly MultipleSettingsSource sut;

        public MultipleSettingsSourceTests()
        {
            Environment.SetEnvironmentVariable("EnvironmentKey", "Environment value");
            Environment.SetEnvironmentVariable("CommonKey", "Environment value for common key");
            var environmentSettingsSource = new EnvironmentSettingsSource();

            var inMemorySettingsSource = new InMemorySettingsSource(
                new Dictionary<string, string>
                {
                    {"InMemoryKey", "In memory value"},
                    {"CommonKey", "In memory value for common key" }
                });

            sut = new MultipleSettingsSource(new List<IRawSettingsSource>
            {
                environmentSettingsSource,
                inMemorySettingsSource
            });
        }

        [Fact]
        public void CanGetSettingFromFirstSource()
        {
            string value;
            var found = sut.TryGetRawSetting("EnvironmentKey", out value);

            Assert.True(found, "Could not find setting in first source");
            Assert.Equal("Environment value", value);
        }

        [Fact]
        public void CanGetSettingFromSecondSource()
        {
            string value;
            var found = sut.TryGetRawSetting("InMemoryKey", out value);

            Assert.True(found, "Could not find setting in second source");
            Assert.Equal("In memory value", value);
        }

        [Fact]
        public void CanGetNonExistingSetting()
        {
            string value;
            var found = sut.TryGetRawSetting("NoSuchKey", out value);

            Assert.False(found, "NoSuchKey was found");
        }

        [Fact]
        public void FirstSourceHasPrecedence()
        {
            string value;
            var found = sut.TryGetRawSetting("CommonKey", out value);

            Assert.True(found, "Could not find common key");
            Assert.Equal("Environment value for common key", value);
        }
    }
}
